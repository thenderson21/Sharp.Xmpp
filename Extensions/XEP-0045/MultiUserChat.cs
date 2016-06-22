using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Sharp.Xmpp.Core;
using Sharp.Xmpp.Extensions.Dataforms;
using Sharp.Xmpp.Im;

namespace Sharp.Xmpp.Extensions
{
    internal class MultiUserChat : XmppExtension, IInputFilter<Im.Message>, IInputFilter<Im.Presence>
    {
        public MultiUserChat(XmppIm im) : base(im)
        {
        }

        /// <summary>
        /// An enumerable collection of XMPP namespaces the extension implements.
        /// </summary>
        /// <remarks>This is used for compiling the list of supported extensions
        /// advertised by the 'MultiUserChat' extension.</remarks>
        public override IEnumerable<string> Namespaces
        {
            get
            {
                return new string[] {
                    MucNs.NsMain
                };
            }
        }

        /// <summary>
        /// The named constant of the Extension enumeration that corresponds to this
        /// extension.
        /// </summary>
        public override Extension Xep
        {
            get
            {
                return Extension.MultiUserChat;
            }
        }

        public event EventHandler<Im.MessageEventArgs> SubjectChanged;

        public RegistrationCallback VoiceRequested;

        public override void Initialize()
        {
            base.Initialize();
        }

        public bool Input(Im.Message stanza)
        {
            // Things that could happen here:
            // Receive Registration Request
            // Receive Voice Request
            // Group Chat Message 
            // Group Chat History
            // Subject Change

            if (stanza.Subject != null)
            {
                SubjectChanged.Raise(this, new Im.MessageEventArgs(stanza.From, stanza));
                return true;
            }
            
            XmlElement xElement = stanza.Data["x"];
            if (xElement != null && xElement.NamespaceURI == "jabber:x:data")
                switch (xElement.FirstChild.Value)
                {
                    default:
                        break;
                    case MucNs.NsRequest:
                        // Invoke Voice Request Submission callback/event.
                        // 8.6 Approving Voice Requests
                        if (VoiceRequested != null)
                        {
                            SubmitForm form = VoiceRequested.Invoke(new RequestForm(xElement));
                            var message = new Core.Message(stanza.From, im.Jid, form.ToXmlElement());
                            SendMessage(message);
                            return true;
                        }
                        break;
                    case MucNs.NsRegister:
                        // Invoke Registration Request Submission callback/event.
                        // 9.9 Approving Registration Requests
                        // I'm unsure on how to implement this.
                        // return true;
                        break;
                }

            // Any message with a body can be managed by the IM extension
            return false;
        }

        public bool Input(Im.Presence stanza)
        {
            // Things that could happen here:
            // Unable to join - No nickname specified / Duplicate nickname exists
            // Service Sends Notice of Membership
            // Service Passes Along Changed Presence
            // Service Updates Nick
            // Invitations Received/WasDeclined

            // Any message with an Availability status can be managed by the Presence extension
            return false;
        }

        /// <summary>
        /// Returns a list of active public chat room messages.
        /// </summary>
        /// <param name="chatService">JID of the chat service (depends on server)</param>
        /// <returns>List of Room JIDs</returns>
        public IEnumerable<RoomInfoBasic> DiscoverRooms(Jid chatService)
        {
            chatService.ThrowIfNull("chatService");
            return QueryRooms(chatService);
        }

        /// <summary>
        /// Returns a list of active public chat room messages.
        /// </summary>
        /// <param name="chatRoom">Existing room info</param>
        /// <returns>Information about room</returns>
        public RoomInfoExtended GetRoomInfo(Jid chatRoom)
        {
            chatRoom.ThrowIfNull("chatRoom");
            return QueryRoom(chatRoom);
        }

        /// <summary>
        /// Joins or creates new room using the specified room
        /// </summary>
        public void JoinRoom(Jid jid, string nickname)
        {
            XmlElement elem = Xml.Element("x", MucNs.NsMain);
            Jid joinRequest = new Jid(jid.Domain, jid.Node, nickname);
            var msg = new Core.Presence(joinRequest, im.Jid, null, null, elem);

            im.SendPresence(new Im.Presence(msg));
        }

        /// <summary>
        /// Requests previous chat room messages.
        /// </summary>
        public void GetMessageLog(Jid target, History options)
        {
            XmlElement elem = Xml.Element("x", MucNs.NsMain);
            elem.Child(options.Element);
            var msg = new Core.Presence(target, im.Jid, null, null, elem);

            im.SendPresence(new Im.Presence(msg));
        }

        /// <summary>
        /// Requests a list of occupants with a specific affiliation.
        /// </summary>
        public IEnumerable<Item> GetMembers(Jid room, Affiliation affiliation)
        {
            return QueryOccupants(room, affiliation);
        }

        /// <summary>
        /// Requests a list of occupants with a specific role.
        /// </summary>
        public IEnumerable<Item> GetMembers(Jid room, Role role)
        {
            return QueryOccupants(room, role);
        }

        /// <summary>
        /// Set your nickname in the room.
        /// </summary>
        public void SetNickName(Jid room, string nickname)
        {
            room.ThrowIfNull("room");
            nickname.ThrowIfNullOrEmpty("nickname");

            Jid request = new Jid(room.Domain, room.Node, nickname);
            var msg = new Core.Presence(request, im.Jid, null, null, null);

            im.SendPresence(new Im.Presence(msg));
        }

        /// <summary>
        /// Allows visitors to request membership to a room.
        /// </summary>
        public void RequestMembership()
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Allows owners and admins to grant privileges to an occupant.
        /// </summary>
        public bool SetPrivilege(Jid room, string nickname, Role privilege, string reason = null)
        {
            return PostPrivilegeChange(room, nickname, privilege, reason);
        }

        /// <summary>
        /// Allows owners and admins to grant privileges to an occupant.
        /// </summary>
        public bool SetPrivilege(Jid room, string nickname, Affiliation privilege, string reason = null)
        {
            return PostPrivilegeChange(room, nickname, privilege, reason);
        }

        public void ModifyRoomConfig(Jid room, RegistrationCallback callback)
        {
            RequestForm form = RequestRoomConfigForm(room);
            SubmitForm submit = callback.Invoke(form);
            SubmitRoomConfigForm(room, submit);
        }

        /// <summary>
        /// Allows moderators (and above) to edit the room subject.
        /// </summary>
        public void EditRoomSubject(Jid room, string subject)
        {
            subject.ThrowIfNull("subject");
            Im.Message message = new Im.Message(room, null, subject, null, MessageType.Groupchat);
            SendMessage(message);
        }

        /// <summary>
        /// Allows owners to destroy the room.
        /// </summary>
        public bool DestroyRoom(Jid room, string reason = null)
        {
            room.ThrowIfNull("room");

            var item = Xml.Element("destroy")
                    .Attr("jid", room.ToString());

            if (!string.IsNullOrWhiteSpace(reason))
                item.Child(Xml.Element("reason").Text(reason));

            var queryElement = Xml.Element("query", MucNs.NsOwner)
                .Child(item);

            Iq iq = im.IqRequest(IqType.Get, room, im.Jid, queryElement);
            return iq.Type == IqType.Result;
        }

        private RequestForm RequestRoomConfigForm(Jid room)
        {
            Iq iq = im.IqRequest(IqType.Get, room, im.Jid, Xml.Element("query", MucNs.NsOwner));
            if (iq.Type != IqType.Result)
                throw new NotSupportedException("Could not query features: " + iq);

            // Parse the result.
            var query = iq.Data["query"];
            if (query == null || query.NamespaceURI != MucNs.NsOwner)
                throw new NotSupportedException("Erroneous response: " + iq);
            return DataFormFactory.Create(query["x"]) as RequestForm;
        }

        private void SubmitRoomConfigForm(Jid room, SubmitForm configForm)
        {
            // Construct the response element.
            var query = Xml.Element("query", MucNs.NsOwner);
            var xml = Xml.Element("x", "jabber:x:data");
            xml.Child(configForm.ToXmlElement());
            query.Child(xml);

            Iq iq = im.IqRequest(IqType.Set, room, im.Jid, query);
            if (iq.Type == IqType.Error)
                throw Util.ExceptionFromError(iq, "The configuration changes could not be completed.");
        }

        private bool PostPrivilegeChange(Jid room, Jid user, Affiliation affiliation, string reason)
        {
            room.ThrowIfNull("room");
            user.ThrowIfNull("user");

            var item = Xml.Element("item")
                    .Attr("affiliation", affiliation.ToString().ToLower())
                    .Attr("jid", user.ToString());

            if (!string.IsNullOrWhiteSpace(reason))
                item.Child(Xml.Element("reason").Text(reason));

            var queryElement = Xml.Element("query", MucNs.NsAdmin)
                .Child(item);

            Iq iq = im.IqRequest(IqType.Get, room, im.Jid, queryElement);
            return iq.Type == IqType.Result;
        }

        private bool PostPrivilegeChange(Jid room, string nickname, Role role, string reason)
        {
            room.ThrowIfNull("room");
            nickname.ThrowIfNull("nickname");

            var item = Xml.Element("item")
                    .Attr("role", role.ToString().ToLower())
                    .Attr("nick", nickname);

            if (!string.IsNullOrWhiteSpace(reason))
                item.Child(Xml.Element("reason").Text(reason));

            var queryElement = Xml.Element("query", MucNs.NsAdmin)
                .Child(item);

            Iq iq = im.IqRequest(IqType.Get, room, im.Jid, queryElement);
            return iq.Type == IqType.Result;
        }

        /// <summary>
        /// Queries for occupants in a room,
        /// This will fail if you do not have permissions.
        /// </summary>
        /// <param name="room">Chat room to query</param>
        /// <param name="affiliation">Queried user affiliation</param>
        /// <returns>An enumerable collection of items of the XMPP entity
        /// with the specified IRoom.</returns>
        /// <exception cref="ArgumentNullException">The IRoom jid parameter
        /// is null.</exception>
        /// <exception cref="NotSupportedException">The query could not be
        /// performed or the response was invalid.</exception>
        private IEnumerable<Item> QueryOccupants(Jid room, Affiliation affiliation)
        {
            room.ThrowIfNull("room");
            var queryElement = Xml.Element("query", MucNs.NsAdmin)
                .Child(Xml.Element("item").Attr("affiliation", affiliation.ToString().ToLower()));

            Iq iq = im.IqRequest(IqType.Get, room, im.Jid, queryElement);
            if (iq.Type != IqType.Result)
                throw new NotSupportedException("Could not query items: " + iq);
            // Parse the result.
            var query = iq.Data["query"];
            if (query == null || query.NamespaceURI != MucNs.NsAdmin)
                throw new NotSupportedException("Erroneous response: " + iq);
            ISet<Item> items = new HashSet<Item>();
            foreach (XmlElement e in query.GetElementsByTagName("item"))
            {
                string _jid = e.GetAttribute("jid"),
                    _affiliation = e.GetAttribute("affiliation"),
                    _nick = e.GetAttribute("nick"),
                    _role = e.GetAttribute("role");
                if (string.IsNullOrEmpty(_jid) | string.IsNullOrEmpty(_affiliation))
                    continue;
                try
                {
                    items.Add(new Item(_affiliation, _jid, _nick, _role));
                }
                catch (ArgumentException)
                {
                    // The JID is malformed, ignore the item.
                }
            }

            return items;
        }

        /// <summary>
        /// Queries for occupants in a room,
        /// This will fail if you do not have permissions.
        /// </summary>
        /// <param name="room">Chat room to query</param>
        /// <param name="role">Queried user role</param>
        /// <returns>An enumerable collection of items of the XMPP entity
        /// with the specified IRoom.</returns>
        /// <exception cref="ArgumentNullException">The IRoom jid parameter
        /// is null.</exception>
        /// <exception cref="NotSupportedException">The query could not be
        /// performed or the response was invalid.</exception>
        private IEnumerable<Item> QueryOccupants(Jid room, Role role)
        {
            room.ThrowIfNull("room");
            var queryElement = Xml.Element("query", MucNs.NsAdmin)
                .Child(Xml.Element("item").Attr("role", role.ToString().ToLower()));

            Iq iq = im.IqRequest(IqType.Get, room, im.Jid, queryElement);
            if (iq.Type != IqType.Result)
                throw new NotSupportedException("Could not query items: " + iq);
            // Parse the result.
            var query = iq.Data["query"];
            if (query == null || query.NamespaceURI != MucNs.NsAdmin)
                throw new NotSupportedException("Erroneous response: " + iq);
            ISet<Item> items = new HashSet<Item>();
            foreach (XmlElement e in query.GetElementsByTagName("item"))
            {
                string _jid = e.GetAttribute("jid"),
                    _affiliation = e.GetAttribute("affiliation"),
                    _nick = e.GetAttribute("nick"),
                    _role = e.GetAttribute("role");
                if (string.IsNullOrEmpty(_jid) | string.IsNullOrEmpty(_affiliation))
                    continue;
                try
                {
                    items.Add(new Item(_affiliation, _jid, _nick, _role));
                }
                catch (ArgumentException)
                {
                    // The JID is malformed, ignore the item.
                }
            }

            return items;
        }

        /// <summary>
        /// Queries the XMPP entity with the specified JID for item information.
        /// </summary>
        /// <param name="jid">The JID of the XMPP entity to query.</param>
        /// <returns>An enumerable collection of items of the XMPP entity
        /// with the specified JID.</returns>
        /// <exception cref="ArgumentNullException">The jid parameter
        /// is null.</exception>
        /// <exception cref="NotSupportedException">The query could not be
        /// performed or the response was invalid.</exception>
        private IEnumerable<RoomInfoBasic> QueryRooms(Jid jid)
        {
            jid.ThrowIfNull("jid");
            Iq iq = im.IqRequest(IqType.Get, jid, im.Jid,
                Xml.Element("query", MucNs.NsRequestItems));
            if (iq.Type != IqType.Result)
                throw new NotSupportedException("Could not query items: " + iq);
            // Parse the result.
            var query = iq.Data["query"];
            if (query == null || query.NamespaceURI != MucNs.NsRequestItems)
                throw new NotSupportedException("Erroneous response: " + iq);
            ISet<RoomInfoBasic> items = new HashSet<RoomInfoBasic>();
            foreach (XmlElement e in query.GetElementsByTagName("item"))
            {
                string _jid = e.GetAttribute("jid"), node = e.GetAttribute("node"),
                    name = e.GetAttribute("name");
                if (String.IsNullOrEmpty(_jid))
                    continue;
                try
                {
                    Jid itemJid = new Jid(_jid);
                    items.Add(new RoomInfoBasic(itemJid, name));
                }
                catch (ArgumentException)
                {
                    // The JID is malformed, ignore the item.
                }
            }
            return items;
        }

        /// <summary>
        /// Queries the XMPP entity with the JID in the specified RoomInfo for item information.
        /// </summary>
        /// <param name="room">Holds the JID of the XMPP entity to query.</param>
        /// <returns>A more detailed description of the specified room.</returns>
        private RoomInfoExtended QueryRoom(Jid room)
        {
            room.ThrowIfNull("roomInfo");
            Iq iq = im.IqRequest(IqType.Get, room, im.Jid,
                Xml.Element("query", MucNs.NsRequestInfo));
            if (iq.Type != IqType.Result)
                throw new NotSupportedException("Could not query features: " + iq);
            // Parse the result.
            var query = iq.Data["query"];
            if (query == null || query.NamespaceURI != MucNs.NsRequestInfo)
                throw new NotSupportedException("Erroneous response: " + iq);

            Identity id = ParseIdentity(query);
            IEnumerable<DataField> features = ParseFields(query, "feature");
            IEnumerable<DataField> fields = ParseFields(query, "field");

            return new RoomInfoExtended(room, id.Name, features, fields);
        }

        /// <summary>
        /// Queries the XMPP entity with the specified JID for identity information.
        /// </summary>
        /// <param name="query">The query result</param>
        /// <returns>The first Identity returned.</returns>
        private Identity ParseIdentity(XmlElement query)
        {
            Identity result = null;

            foreach (XmlElement e in query.GetElementsByTagName("identity"))
            {
                string cat = e.GetAttribute("category");
                string type = e.GetAttribute("type");
                string name = e.GetAttribute("name");

                if (!String.IsNullOrEmpty(cat) && !String.IsNullOrEmpty(type))
                {
                    result = new Identity(cat, type,
                        String.IsNullOrEmpty(name) ? null : name);
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Parses the Identity element and returns a list of the identity's features.
        /// </summary>
        /// <param name="query">The query result</param>
        /// <param name="tagName">The tag name of the objects</param>
        /// <returns>An enumerable collection of DataFields</returns>
        private IEnumerable<DataField> ParseFields(XmlElement query, string tagName)
        {
            ISet<DataField> fields = new HashSet<DataField>();

            foreach (XmlElement f in query.GetElementsByTagName(tagName))
            {
                fields.Add((new DataField(f)));
            }

            return fields;
        }

        public void SendMessage(Core.Message message)
        {
            im.SendMessage(new Im.Message(message));
        }
    }
}
