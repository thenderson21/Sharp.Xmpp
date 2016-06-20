using System;
using System.Collections.Generic;
using System.Globalization;

namespace Sharp.Xmpp.Extensions
{
    /// <summary>
    /// Represents an instance of a conference room as defined in XEP-0045.
    /// </summary>
    public interface IRoom : IRoomBasic
    {
        /// <summary>
        /// The anonymity level of the room.
        /// </summary>
        RoomAnonymity Anonymity { get; }

        /// <summary>
        /// The owner or owners of the room.
        /// </summary>
        IEnumerable<Jid> ContactAddresses { get; }

        /// <summary>
        /// Datetime the room was created.
        /// </summary>
        DateTime? CreationDate { get; }

        /// <summary>
        /// The description of the room.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The language of the room.
        /// </summary>
        CultureInfo Language { get; }

        /// <summary>
        /// An associated LDAP group that defines room membership.
        /// </summary>
        string LDAPGroup { get; }

        /// <summary>
        /// The language of the room.
        /// </summary>
        string LogUrl { get; }

        /// <summary>
        /// The maximum number historic of messages a room will display.
        /// </summary>
        int MaxHistoryFetch { get; }

        /// <summary>
        /// The moderation level of the room.
        /// </summary>
        RoomModeration Moderation { get; }

        /// <summary>
        /// The number of occupants in the room.
        /// </summary>
        int NumberOfOccupants { get; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<Jid> Occupants { get; }

        /// <summary>
        /// The persistence level of the room.
        /// </summary>
        RoomPersistence Persistence { get; }

        /// <summary>
        /// The privacy level of the room.
        /// </summary>
        RoomPrivacy Privacy { get; }

        /// <summary>
        /// The protection level of the room.
        /// </summary>
        RoomProtection Protection { get; }

        /// <summary>
        /// The subject of the room.
        /// </summary>
        string Subject { get; }

        /// <summary>
        /// The visibility of the room.
        /// </summary>
        RoomVisibility Visibility { get; }
    }
}