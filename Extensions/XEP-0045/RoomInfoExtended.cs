using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Xmpp.Extensions.XEP_0045
{
    /// <summary>
    /// Room information provided upon inspection.
    /// </summary>
    public class RoomInfoExtended : RoomInfoBasic
    {
        private string description;
        private string subject;
        private int occupants;
        private DateTime? creationDate;

        private RoomVisibility visibility;
        private RoomPersistence persistence;
        private RoomProtection protection;
        private RoomPrivacy privacy;
        private RoomModeration moderation;
        private RoomAnonymity anonymity;

        internal RoomInfoExtended(Jid jid, string name, string description, string subject) 
            : base(jid, name)
        {
            RoomVisibility = RoomVisibility.Public;
            RoomPersistence = RoomPersistence.Temporary;
            RoomProtection = RoomProtection.Unsecured;
            RoomPrivacy = RoomPrivacy.Open;
            RoomModeration = RoomModeration.Unmoderated;
            RoomAnonymity = RoomAnonymity.NonAnonymous;
            RoomDescription = description;
            RoomSubject = subject;
            NumberOfOccupants = 0;
            CreationDate = DateTime.UtcNow;
        }

        internal RoomInfoExtended(Jid jid, string name, IEnumerable<Feature> features,
            string description, string subject, int occupants, DateTime? creation)
             : base(jid, name)
        {
            RoomVisibility = RoomVisibility.Undefined;
            RoomPersistence = RoomPersistence.Undefined;
            RoomProtection = RoomProtection.Undefined;
            RoomPrivacy = RoomPrivacy.Undefined;
            RoomModeration = RoomModeration.Undefined;
            RoomAnonymity = RoomAnonymity.Undefined;
            RoomDescription = string.IsNullOrEmpty(description) ? string.Empty : description;
            RoomSubject = string.IsNullOrEmpty(subject) ? string.Empty : subject;
            NumberOfOccupants = occupants;
            creationDate = creation;

            IntialiseRoomSettings(features);
        }

        internal RoomInfoExtended(RoomInfoBasic room, string name, IEnumerable<Feature> features,
            string description, string subject, int occupants, DateTime? creation)
             : this(room.Jid, room.RoomName, features, description, subject,  occupants,  creation)
        {
            if (RoomName != name)
                RoomName = name;
        }

        /// <summary>
        /// The visibility of the room.
        /// </summary>
        public RoomVisibility RoomVisibility
        {
            get { return visibility; }
            protected set { visibility = value; }
        }

        /// <summary>
        /// The persistence level of the room.
        /// </summary>
        public RoomPersistence RoomPersistence
        {
            get { return persistence; }
            protected set { persistence = value; }
        }

        /// <summary>
        /// The protection level of the room.
        /// </summary>
        public RoomProtection RoomProtection
        {
            get { return protection; }
            protected set { protection = value; }
        }

        /// <summary>
        /// The privacy level of the room.
        /// </summary>
        public RoomPrivacy RoomPrivacy
        {
            get { return privacy; }
            protected set { privacy = value; }
        }

        /// <summary>
        /// The moderation level of the room.
        /// </summary>
        public RoomModeration RoomModeration
        {
            get { return moderation; }
            protected set { moderation = value; }
        }

        /// <summary>
        /// The anonymity level of the room.
        /// </summary>
        public RoomAnonymity RoomAnonymity
        {
            get { return anonymity; }
            protected set { anonymity = value; }
        }

        /// <summary>
        /// The description of the room.
        /// </summary>
        public string RoomDescription
        {
            get { return description; }
            protected set { description = value; }
        }

        /// <summary>
        /// The subject of the room.
        /// </summary>
        public string RoomSubject
        {
            get { return subject; }
            protected set { subject = value; }
        }

        /// <summary>
        /// The number of occupants in the room.
        /// </summary>
        public int NumberOfOccupants
        {
            get { return occupants; }
            protected set { occupants = value; }
        }

        /// <summary>
        /// Datetime the room was created.
        /// </summary>
        public DateTime? CreationDate
        {
            get { return creationDate; }
            protected set { creationDate = value; }
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
                        RoomProtection = RoomProtection.Unsecured;
                        break;
                    case "muc_passwordprotected":
                        RoomProtection = RoomProtection.PasswordProtected;
                        break;
                    case "muc_public":
                        RoomVisibility = RoomVisibility.Public;
                        break;
                    case "muc_hidden":
                        RoomVisibility = RoomVisibility.Hidden;
                        break;
                    case "muc_temporary":
                        RoomPersistence = RoomPersistence.Temporary;
                        break;
                    case "muc_persistent":
                        RoomPersistence = RoomPersistence.Persistent;
                        break;
                    case "muc_open":
                        RoomPrivacy = RoomPrivacy.Open;
                        break;
                    case "muc_membersonly":
                        RoomPrivacy = RoomPrivacy.MembersOnly;
                        break;
                    case "muc_unmoderated":
                        RoomModeration = RoomModeration.Unmoderated;
                        break;
                    case "muc_moderated":
                        RoomModeration = RoomModeration.Moderated;
                        break;
                    case "muc_nonanonymous":
                        RoomAnonymity = RoomAnonymity.NonAnonymous;
                        break;
                    case "muc_semianonymous":
                        RoomAnonymity = RoomAnonymity.SemiAnonymous;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
