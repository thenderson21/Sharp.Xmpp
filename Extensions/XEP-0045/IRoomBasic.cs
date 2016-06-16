using System.Collections.Generic;
using System.Globalization;

namespace Sharp.Xmpp.Extensions.XEP_0045
{
    /// <summary>
    /// Represents an instance of a conference room as defined in XEP-0045.
    /// </summary>
    public interface IRoomBasic
    {
        /// <summary>
        /// Room Identifier
        /// </summary>
        Jid Jid { get; }

        /// <summary>
        /// The name of the room.
        /// </summary>
        string Name { get; }
    }
}