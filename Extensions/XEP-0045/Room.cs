using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Xmpp.Extensions.XEP_0045
{
    /// <summary>
    /// Represents an instance of a conference room as defined in XEP-0045.
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
        /// </summary>
        {
        }
        }
        }
    }
}
