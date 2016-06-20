using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Xmpp.Extensions
{
    /// <summary>
    /// The most basic form of a chat room
    /// </summary>
    public class RoomInfoBasic : IRoomBasic
    {
        private Jid jid;
        private string name;

        /// <summary>
        /// Basic room info
        /// </summary>
        /// <param name="jid">Room identifier</param>
        /// <param name="name">Room name</param>
        public RoomInfoBasic(Jid jid, string name = null)
        {
            jid.ThrowIfNull("jid");
            Jid = jid;

            if (string.IsNullOrWhiteSpace(name))
                Name = jid.Node;
            else
                Name = name;
        }

        /// <summary>
        /// Create from an existing room info
        /// </summary>
        /// <param name="room">Existing room info</param>
        public RoomInfoBasic(RoomInfoBasic room)
        {
            Jid = room.Jid;
            Name = room.Name;
        }

        /// <summary>
        /// The JID of the room.
        /// </summary>
        public Jid Jid
        {
            get { return jid; }
            protected set { jid = value; }
        }

        /// <summary>
        /// The name of the room.
        /// </summary>
        public string Name
        {
            get { return name; }
            protected set { name = value; }
        }
    }
}
