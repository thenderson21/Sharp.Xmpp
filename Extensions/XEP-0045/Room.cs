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
    public class Room : RoomInfoExtended, IRoom
    {
        private bool canChangeSubject;
        private IEnumerable<Jid> contactAddresses;
        private IEnumerable<Jid> occupants;
        private string ldapGroup;
        private CultureInfo language;
        private Uri logUrl;
        private int maxHistoryFetch;
        private int pubSubNode;


        public Room(Jid jid, string name, string description, string subject) 
            : base(jid, name, description, subject)
        {
        }

        /// <summary>
        /// The owner or owners of the room.
        /// </summary>
        public IEnumerable<Jid> ContactAddresses
        {
            get { return contactAddresses; }
            private set { contactAddresses = value; }
        }

        /// <summary>
        /// An associated LDAP group that defines room membership.
        /// </summary>
        public string LDAPGroup
        {
            get { return ldapGroup; }
            private set { ldapGroup = value; }
        }

        /// <summary>
        /// The language of the room.
        /// </summary>
        public CultureInfo Language
        {
            get { return language; }
            private set { language = value; }
        }

    }
}
