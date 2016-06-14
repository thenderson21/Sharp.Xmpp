using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Xmpp.Extensions.XEP_0045
{
    /// <summary>
    /// Represents an instance of a conference room.
    /// </summary>
    public class Room
    {
        // Disco Info Result
        private string name;
        private RoomVisibility visibility;
        private RoomPersistence persistence;
        private RoomProtection protection;
        private RoomPrivacy privacy;
        private RoomModeration moderation;
        private RoomAnonymity anonymity;

        // Extended Disco Info Result
        private string description;
        private bool canChangeSubject;
        private string subject;
        private IEnumerable<Jid> contactAddresses;
        private IEnumerable<Jid> occupants;
        private string ldapGroup;
        private CultureInfo language;
        private Uri logUrl;
        private int maxHistoryFetch;
        private int pubSubNode;

        /// <summary>
        /// The name of the room.
        /// </summary>
        public string RoomName
        {
            get { return name; }
        }

        /// <summary>
        /// The visibility of the room.
        /// </summary>
        public RoomVisibility RoomVisibility
        {
            get { return visibility; }
        }

        /// <summary>
        /// The persistence level of the room.
        /// </summary>
        public RoomPersistence RoomPersistence
        {
            get { return persistence; }
        }

        /// <summary>
        /// The protection level of the room.
        /// </summary>
        public RoomProtection RoomProtection
        {
            get { return protection; }
        }

        /// <summary>
        /// The privacy level of the room.
        /// </summary>
        public RoomPrivacy RoomPrivacy
        {
            get { return privacy; }
        }

        /// <summary>
        /// The moderation level of the room.
        /// </summary>
        public RoomModeration RoomModeration
        {
            get { return moderation; }
        }

        /// <summary>
        /// The anonymity level of the room.
        /// </summary>
        public RoomAnonymity RoomAnonymity
        {
            get { return anonymity; }
        }

        /// <summary>
        /// The description of the room.
        /// </summary>
        public string RoomDescription
        {
            get { return description; }
        }

        /// <summary>
        /// The subject of the room.
        /// </summary>
        public string RoomSubject
        {
            get { return subject; }
        }

        /// <summary>
        /// The owner or owners of the room.
        /// </summary>
        public IEnumerable<Jid> ContactAddresses
        {
            get { return contactAddresses; }
        }

        /// <summary>
        /// An associated LDAP group that defines room membership.
        /// </summary>
        public string LDAPGroup
        {
            get { return ldapGroup; }
        }

        /// <summary>
        /// The language of the room.
        /// </summary>
        public CultureInfo Language
        {
            get { return language; }
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
