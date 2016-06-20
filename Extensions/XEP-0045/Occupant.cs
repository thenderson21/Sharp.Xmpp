using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Xmpp.Extensions.XEP_0045
{
    /// <summary>
    /// Represents an participant in a group chat.
    /// </summary>
    public class Occupant
    {
        /// <summary>
        /// The real identifier of the participant.
        /// </summary>
        public Jid RealJid { get; set; }

        /// <summary>
        /// The real identifier of the participant.
        /// </summary>
        public Jid GroupJid { get; set; }

        /// <summary>
        /// The participants nickname.
        /// </summary>
        public string Nickname
        {
            get
            {
                return GroupJid == null ? null : GroupJid.Resource;
            }
        }

        /// <summary>
        /// Permission level of the participant.
        /// </summary>
        public Role Role { get; set; }
    }
}
