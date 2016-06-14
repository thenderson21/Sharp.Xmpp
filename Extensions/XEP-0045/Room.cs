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
    public class Room : IRoom
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
        /// Empty constructor
        /// </summary>
        /// <param name="name">Room Name</param>
        public Room(string name)
        {
            this.name = name;
            this.visibility = RoomVisibility.Public;
            this.persistence = RoomPersistence.Temporary;
            this.protection = RoomProtection.Unsecured;
            this.privacy = RoomPrivacy.Open;
            this.moderation = RoomModeration.Unmoderated;
            this.anonymity = RoomAnonymity.NonAnonymous;
        }

        /// <summary>
        /// Internal constructor used for room info
        /// </summary>
        /// <param name="name">Room Name</param>
        /// <param name="features">Room Features</param>
        internal Room(string name, IEnumerable<Feature> features) : this(name)
        {
            this.IntialiseRoomSettings(features);
        }

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
        /// Initialises the room settings using the provided features.
        /// </summary>
        /// <param name="features">Room features</param>
        private void IntialiseRoomSettings(IEnumerable<Feature> features)
        {
            foreach (Feature f in features)
            {
                switch (f.Var)
                {
                    case "muc_unsecured":
                        protection = RoomProtection.Unsecured;
                        break;
                    case "muc_passwordprotected":
                        protection = RoomProtection.PasswordProtected;
                        break;
                    case "muc_public":
                        visibility = RoomVisibility.Public;
                        break;
                    case "muc_hidden":
                        visibility = RoomVisibility.Hidden;
                        break;
                    case "muc_temporary":
                        persistence = RoomPersistence.Temporary;
                        break;
                    case "muc_persistent":
                        persistence = RoomPersistence.Persistent;
                        break;
                    case "muc_open":
                        privacy = RoomPrivacy.Open;
                        break;
                    case "muc_membersonly":
                        privacy = RoomPrivacy.MembersOnly;
                        break;
                    case "muc_unmoderated":
                        moderation = RoomModeration.Unmoderated;
                        break;
                    case "muc_moderated":
                        moderation = RoomModeration.Moderated;
                        break;
                    case "muc_nonanonymous":
                        anonymity = RoomAnonymity.NonAnonymous;
                        break;
                    case "muc_semianonymous":
                        anonymity = RoomAnonymity.SemiAnonymous;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
