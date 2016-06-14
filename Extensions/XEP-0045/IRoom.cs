using System.Collections.Generic;
using System.Globalization;

namespace Sharp.Xmpp.Extensions.XEP_0045
{
    /// <summary>
    /// Represents an instance of a conference room as defined in XEP-0045.
    /// </summary>
    public interface IRoom
    {

        /// <summary>
        /// The owner or owners of the room.
        /// </summary>
        IEnumerable<Jid> ContactAddresses { get; }

        /// <summary>
        /// The language of the room.
        /// </summary>
        CultureInfo Language { get; }

        /// <summary>
        /// An associated LDAP group that defines room membership.
        /// </summary>
        string LDAPGroup { get; }

        /// <summary>
        /// The anonymity level of the room.
        /// </summary>
        RoomAnonymity RoomAnonymity { get; }

        /// <summary>
        /// The description of the room.
        /// </summary>
        string RoomDescription { get; }

        /// <summary>
        /// The moderation level of the room.
        /// </summary>
        RoomModeration RoomModeration { get; }


        /// <summary>
        /// The name of the room.
        /// </summary>
        string RoomName { get; }

        /// <summary>
        /// The persistence level of the room.
        /// </summary>
        RoomPersistence RoomPersistence { get; }

        /// <summary>
        /// The privacy level of the room.
        /// </summary>
        RoomPrivacy RoomPrivacy { get; }

        /// <summary>
        /// The protection level of the room.
        /// </summary>
        RoomProtection RoomProtection { get; }

        /// <summary>
        /// The subject of the room.
        /// </summary>
        string RoomSubject { get; }

        /// <summary>
        /// The visibility of the room.
        /// </summary>
        RoomVisibility RoomVisibility { get; }
    }
}