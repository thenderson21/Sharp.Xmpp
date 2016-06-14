using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp.Xmpp.Core;
using Sharp.Xmpp.Im;

namespace Sharp.Xmpp.Extensions.XEP_0045
{
    internal class MultiUserChat : XmppExtension, IInputFilter<Im.Message>
    {
        private ServiceDiscovery sd;

        public MultiUserChat(XmppIm im, ServiceDiscovery sd) : base(im)
        {
            this.sd = sd;
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
                    "http://jabber.org/protocol/disco#rooms",
                    "http://jabber.org/protocol/muc"
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

        public bool Input(Im.Message stanza)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a list of active public chat room messages.
        /// </summary>
        public IEnumerable<Jid> DiscoverRooms(Jid chatService)
        {
            return sd.GetItems(chatService)
                .Select(x => x.Jid)
                .ToList();
        }

        /// <summary>
        /// Returns a list of active public chat room messages.
        /// </summary>
        public Room GetRoomInfo(Jid chatRoom)
        {
            return sd.GetIdentities(chatRoom)
                .Select(x => new Room(x.Name, x.Features))
                .FirstOrDefault();
        }

        /// <summary>
        /// Returns previous chat room messages.
        /// </summary>
        public void GetMessageLog()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns occupants in the room.
        /// </summary>
        public void GetOccupants()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns moderators in the room.
        /// </summary>
        public void GetModerators()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns admins in the room.
        /// </summary>
        public void GetAdmins()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns owners in the room.
        /// </summary>
        public void GetOwners()
        {
            throw new NotImplementedException();
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
        /// Allows moderators (and above) to grant membership to users.
        /// </summary>
        /// <param name="jid">User Identifier</param>
        public void GrantMembership(Jid jid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows moderators (and above) to revoke membership to users.
        /// </summary>
        /// <param name="jid">User Identifier</param>
        public void RevokeMembership(Jid jid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows moderators (and above) to kick occupants from the room.
        /// </summary>
        /// <param name="jid">User Identifier</param>
        public void KickOccupant(Jid jid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows admins to view the ban list.
        /// </summary>
        public void GetBanList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows admins to ban an occupant.
        /// </summary>
        /// <param name="jid">User Identifier</param>
        public void Ban(Jid jid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows admins to unban an occupant.
        /// </summary>
        /// <param name="jid">User Identifier</param>
        public void Unban(Jid jid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a list of occupants with voice privileges.
        /// </summary>
        public void GetVoiceList()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows admins to grant voice permissions to occupant.
        /// </summary>
        /// <param name="jid">User Identifier</param>
        public void GrantVoice(Jid jid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows admins to revoke voice permissions to occupant.
        /// </summary>
        /// <param name="jid">User Identifier</param>
        public void RevokeVoice(Jid jid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows owners and admins to grant privileges to an occupant.
        /// </summary>
        public void GrantPrivilege()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Allows owners and admins to revoke privileges to an occupant.
        /// </summary>
        public void RevokePrivilege()
        {
            throw new NotImplementedException();
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
        public void DestroyRoom()
        {
            throw new NotImplementedException();
        }
    }
}
