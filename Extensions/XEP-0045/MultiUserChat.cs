using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Sharp.Xmpp.Core;
using Sharp.Xmpp.Im;

namespace Sharp.Xmpp.Extensions
{
    internal class MultiUserChat : XmppExtension
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

        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Returns a list of active public chat room messages.
        /// </summary>
        /// <param name="chatService">JID of the chat service (depends on server)</param>
        /// <returns>List of Room JIDs</returns>
        public IEnumerable<IRoomBasic> DiscoverRooms(Jid chatService)
        {
            chatService.ThrowIfNull("chatService");
            return QueryRooms(chatService);
        }

        /// <summary>
        /// Returns a list of active public chat room messages.
        /// </summary>
        /// <param name="chatRoom">Existing room info</param>
        /// <returns>Information about room</returns>
        public IRoom GetRoomInfo(IRoomBasic chatRoom)
        {
            chatRoom.ThrowIfNull("chatRoom");
            return QueryRoom(chatRoom);
        }

        /// <summary>
        /// Joins or creates new room using the specified room
        /// </summary>
        public void JoinRoom(IRoomBasic room, string nickname)
        {
            JoinRoom(room.Jid, nickname);
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
        public void GetMembers(IRoomBasic room, Affiliation affiliation)
        {
            var occupants = QueryOccupants(room, affiliation);

            // TODO: convert list into an enumerable something to go to a viewmodel.
        }

        /// <summary>
        /// Requests a list of occupants with a specific role.
        /// </summary>
        public void GetMembers(IRoomBasic room, Role role)
        {
            var occupants = QueryOccupants(room, role);

            // TODO: convert list into an enumerable something to go to a viewmodel.
        }

        /// <summary>
        /// Set your nickname in the room.
        /// </summary>
        public void SetNickName()
        {
            throw new NotImplementedException();
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
        public bool SetPrivilige(IRoomBasic room, string nickname, Role privilige, string reason = null)
        {
            return PostPriviligeChange(room, nickname, privilige, reason);
        }

        /// <summary>
        /// Allows owners and admins to grant privileges to an occupant.
        /// </summary>
        public bool SetPrivilige(IRoomBasic room, string nickname, Affiliation privilige, string reason = null)
        {
            return PostPriviligeChange(room, nickname, privilige, reason);
        }

        /// <summary>
        /// Allows owners to modify the room name.
        /// </summary>
        public void EditRoomName()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows owners to modify the room description.
        /// </summary>
        public void EditRoomDescription()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows moderators (and above) to edit the room subject.
        /// </summary>
        public void EditRoomSubject()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows owners to limit the number of occupants in a room.
        /// </summary>
        public void EditRoomSize()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows owners to destroy the room.
        /// </summary>
        public bool DestroyRoom(IRoomBasic room, string reason = null)
        {
            room.ThrowIfNull("room");

            var item = Xml.Element("destroy")
                    .Attr("jid", room.Jid.ToString());

            if (!string.IsNullOrWhiteSpace(reason))
                item.Child(Xml.Element("reason").Text(reason));

            var queryElement = Xml.Element("query", MucNs.NsOwner)
                .Child(item);

            Iq iq = im.IqRequest(IqType.Get, room.Jid, im.Jid, queryElement);
            return iq.Type == IqType.Result;
        }

        private bool PostPriviligeChange(IRoomBasic room, Jid user, Affiliation affiliation, string reason)
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

            Iq iq = im.IqRequest(IqType.Get, room.Jid, im.Jid, queryElement);
            return iq.Type == IqType.Result;
        }

        private bool PostPriviligeChange(IRoomBasic room, string nickname, Role role, string reason)
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

            Iq iq = im.IqRequest(IqType.Get, room.Jid, im.Jid, queryElement);
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
        private IEnumerable<Item> QueryOccupants(IRoomBasic room, Affiliation affiliation)
        {
            room.ThrowIfNull("room");
            var queryElement = Xml.Element("query", MucNs.NsAdmin)
                .Child(Xml.Element("item").Attr("affiliation", affiliation.ToString().ToLower()));

            Iq iq = im.IqRequest(IqType.Get, room.Jid, im.Jid, queryElement);
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
        private IEnumerable<Item> QueryOccupants(IRoomBasic room, Role role)
        {
            room.ThrowIfNull("room");
            var queryElement = Xml.Element("query", MucNs.NsAdmin)
                .Child(Xml.Element("item").Attr("role", role.ToString().ToLower()));

            Iq iq = im.IqRequest(IqType.Get, room.Jid, im.Jid, queryElement);
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
                Xml.Element("query", "http://jabber.org/protocol/disco#items"));
            if (iq.Type != IqType.Result)
                throw new NotSupportedException("Could not query items: " + iq);
            // Parse the result.
            var query = iq.Data["query"];
            if (query == null || query.NamespaceURI != "http://jabber.org/protocol/disco#items")
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
        /// <param name="roomInfo">Holds the JID of the XMPP entity to query.</param>
        /// <returns>A more detailed description of the specified room.</returns>
        private RoomInfoExtended QueryRoom(IRoomBasic roomInfo)
        {
            roomInfo.ThrowIfNull("roomInfo");
            Iq iq = im.IqRequest(IqType.Get, roomInfo.Jid, im.Jid,
                Xml.Element("query", "http://jabber.org/protocol/disco#info"));
            if (iq.Type != IqType.Result)
                throw new NotSupportedException("Could not query features: " + iq);
            // Parse the result.
            var query = iq.Data["query"];
            if (query == null || query.NamespaceURI != "http://jabber.org/protocol/disco#info")
                throw new NotSupportedException("Erroneous response: " + iq);

            Identity id = ParseIdentity(query);
            IEnumerable<Feature> features = ParseFeatures(query);
            IEnumerable<Field> fields = ParseFields(query);

            return new RoomInfoExtended(roomInfo, id.Name, features, fields);
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
        /// <returns>An enumerable collection of features</returns>
        private IEnumerable<Feature> ParseFeatures(XmlElement query)
        {
            ISet<Feature> features = new HashSet<Feature>();
            foreach (XmlElement f in query.GetElementsByTagName("feature"))
            {
                string var = f.GetAttribute("var");
                if (string.IsNullOrEmpty(var))
                    continue;
                features.Add(new Feature(var));
            }

            return features;
        }

        private IEnumerable<Field> ParseFields(XmlElement query)
        {
            ISet<Field> fields = new HashSet<Field>();

            foreach (XmlElement e in query.GetElementsByTagName("field"))
            {
                string var = e.GetAttribute("var");
                string label = e.GetAttribute("label");
                string value = e.InnerText;

                if (string.IsNullOrEmpty(var) || string.IsNullOrEmpty(label) || string.IsNullOrEmpty(value))
                    continue;

                fields.Add(new Field(var, label, value));
            }

            return fields;
        }
    }
}
