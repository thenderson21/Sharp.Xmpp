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
            Visibility = RoomVisibility.Public;
            Persistence = RoomPersistence.Temporary;
            Protection = RoomProtection.Unsecured;
            Privacy = RoomPrivacy.Open;
            Moderation = RoomModeration.Unmoderated;
            Anonymity = RoomAnonymity.NonAnonymous;
            Description = description;
            Subject = subject;
            NumberOfOccupants = 0;
            CreationDate = DateTime.UtcNow;
        }

        internal RoomInfoExtended(Jid jid, string name, IEnumerable<Feature> features,
            string description, string subject, int occupants, DateTime? creation)
             : base(jid, name)
        {
            Visibility = RoomVisibility.Undefined;
            Persistence = RoomPersistence.Undefined;
            Protection = RoomProtection.Undefined;
            Privacy = RoomPrivacy.Undefined;
            Moderation = RoomModeration.Undefined;
            Anonymity = RoomAnonymity.Undefined;
            Description = string.IsNullOrEmpty(description) ? string.Empty : description;
            Subject = string.IsNullOrEmpty(subject) ? string.Empty : subject;
            NumberOfOccupants = occupants;
            creationDate = creation;

            IntialiseRoomSettings(features);
        }

        internal RoomInfoExtended(RoomInfoBasic room, string name, IEnumerable<Feature> features,
            string description, string subject, int occupants, DateTime? creation)
             : this(room.Jid, room.Name, features, description, subject,  occupants,  creation)
        {
            if (Name != name)
                Name = name;
        }

        /// <summary>
        /// The visibility of the room.
        /// </summary>
        public RoomVisibility Visibility
        {
            get { return visibility; }
            protected set { visibility = value; }
        }

        /// <summary>
        /// The persistence level of the room.
        /// </summary>
        public RoomPersistence Persistence
        {
            get { return persistence; }
            protected set { persistence = value; }
        }

        /// <summary>
        /// The protection level of the room.
        /// </summary>
        public RoomProtection Protection
        {
            get { return protection; }
            protected set { protection = value; }
        }

        /// <summary>
        /// The privacy level of the room.
        /// </summary>
        public RoomPrivacy Privacy
        {
            get { return privacy; }
            protected set { privacy = value; }
        }

        /// <summary>
        /// The moderation level of the room.
        /// </summary>
        public RoomModeration Moderation
        {
            get { return moderation; }
            protected set { moderation = value; }
        }

        /// <summary>
        /// The anonymity level of the room.
        /// </summary>
        public RoomAnonymity Anonymity
        {
            get { return anonymity; }
            protected set { anonymity = value; }
        }

        /// <summary>
        /// The description of the room.
        /// </summary>
        public string Description
        {
            get { return description; }
            protected set { description = value; }
        }

        /// <summary>
        /// The subject of the room.
        /// </summary>
        public string Subject
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
                        Protection = RoomProtection.Unsecured;
                        break;
                    case "muc_passwordprotected":
                        Protection = RoomProtection.PasswordProtected;
                        break;
                    case "muc_public":
                        Visibility = RoomVisibility.Public;
                        break;
                    case "muc_hidden":
                        Visibility = RoomVisibility.Hidden;
                        break;
                    case "muc_temporary":
                        Persistence = RoomPersistence.Temporary;
                        break;
                    case "muc_persistent":
                        Persistence = RoomPersistence.Persistent;
                        break;
                    case "muc_open":
                        Privacy = RoomPrivacy.Open;
                        break;
                    case "muc_membersonly":
                        Privacy = RoomPrivacy.MembersOnly;
                        break;
                    case "muc_unmoderated":
                        Moderation = RoomModeration.Unmoderated;
                        break;
                    case "muc_moderated":
                        Moderation = RoomModeration.Moderated;
                        break;
                    case "muc_nonanonymous":
                        Anonymity = RoomAnonymity.NonAnonymous;
                        break;
                    case "muc_semianonymous":
                        Anonymity = RoomAnonymity.SemiAnonymous;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
