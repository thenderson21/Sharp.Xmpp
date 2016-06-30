using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Sharp.Xmpp.Extensions
{
    internal class MucError : Im.Presence
    {
        /// <summary>This doesn't really matter because we are only receiving it, just potential future proofing.</summary>
        protected override string RootElementName { get { return "presence";  } }
        
        public MucError(Im.Presence presence) : base(presence)
        {
        }

        public Jid By
        {
            get
            {
                XmlElement node = GetNode("x", "error");
                string v = node == null ? null : node.GetAttribute("by");
                return string.IsNullOrEmpty(v) ? null : new Jid(v);
            }
        }

        public ErrorType ErrorType
        {
            get
            {
                XmlElement node = GetNode("x", "error");
                string v = node == null ? null : node.GetAttribute("type");

                ErrorType error;
                const bool ignoreCase = true;

                // It should always parse, otherwise the message doesn't meet the protocol.
                if (!string.IsNullOrEmpty(v) || !Enum.TryParse(v, ignoreCase, out error))
                    error = ErrorType.Cancel;

                return error;
            }
        }

        public ErrorCondition ErrorCondition
        {
            get
            {
                string nodeName = GetNode("x", "error")?.FirstChild.Name.Remove('-');

                ErrorCondition reason;
                const bool ignoreCase = true;

                // It should always parse, otherwise the message doesn't meet the protocol.
                if (!string.IsNullOrEmpty(nodeName) || !Enum.TryParse(nodeName, ignoreCase, out reason))
                    reason = ErrorCondition.BadRequest;

                return reason;
            }
        }

        public static bool IsError(Im.Presence presence)
        {
            bool isType = presence.Type == Im.PresenceType.Error;

            var xElement = presence.Data["x"];
            bool isMucProtocol = xElement != null && xElement.NamespaceURI == MucNs.NsMain;

            return isType && isMucProtocol;
        }
    }
}
