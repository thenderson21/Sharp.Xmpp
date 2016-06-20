using System;
using System.Xml;

namespace Sharp.Xmpp.Extensions.XEP_0045
{
    /// <summary>
    /// Implements the message history request object as described in XEP-0045.
    /// </summary>
    public class History
    {
        private int? maxChars;
        private int? maxStanzas;
        private int? seconds;
        private DateTime? since;

        /// <summary>
        /// Returns the history element used in presence messages for group chat.
        /// </summary>
        public XmlElement Element
        {
            get
            {
                XmlElement element = Xml.Element("history");

                if (maxChars.HasValue)
                    element.Attr("maxchars", maxChars.ToString());
                else if (maxStanzas.HasValue)
                    element.Attr("maxstanzas", maxStanzas.ToString());
                else if (seconds.HasValue)
                    element.Attr("seconds", seconds.ToString());
                else if (since.HasValue)
                    element.Attr("since", since.Value.ToUniversalTime()
                        .ToString("yyyy-MM-ddTHH:mm:ssZ"));
                else
                    element.Attr("maxchars", "0");

                return element;
            }
        }

        /// <summary>
        /// Limit the total number of characters in the history to "X" 
        /// (where the character count is the characters of the complete XML stanzas,
        /// not only their XML character data).
        /// </summary>
        public int? MaxChars
        {
            get { return maxChars; }
            set { maxChars = value; }
        }

        /// <summary>
        /// Limit the total number of messages in the history to "X".
        /// </summary>
        public int? MaxStanzas
        {
            get { return maxStanzas; }
            set { maxStanzas = value; }
        }

        /// <summary>
        /// Send only the messages received in the last "X" seconds.
        /// </summary>
        public int? Seconds
        {
            get { return seconds; }
            set { seconds = value; }
        }

        /// <summary>
        /// Send only the messages received since the UTC datetime specified.
        /// </summary>
        public DateTime? Since
        {
            get { return since; }
            set { since = value; }
        }
    }
}
