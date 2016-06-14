using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp.Xmpp.Im;

namespace Sharp.Xmpp.Extensions.XEP_0045
{
    internal class MultiUserChat : XmppExtension//, IInputFilter<Im.Message>
    {
        public MultiUserChat(XmppIm im) : base(im)
        {
        }


        /// <summary>
        /// An enumerable collection of XMPP namespaces the extension implements.
        /// </summary>
        /// <remarks>This is used for compiling the list of supported extensions
        /// advertised by the 'MultiUserChat' extension.</remarks>
        public override IEnumerable<string> Namespaces
        {
            get
            {
                return new string[] {
                    "http://jabber.org/protocol/disco#info",
                    "http://jabber.org/protocol/disco#items",
                    "http://jabber.org/protocol/disco#rooms",
                    "http://jabber.org/protocol/muc"
                };
            }
        }

        /// <summary>
        /// The named constant of the Extension enumeration that corresponds to this
        /// extension.
        /// </summary>
        public override Extension Xep
        {
            get
            {
                return Extension.MultiUserChat;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }

    }
}
