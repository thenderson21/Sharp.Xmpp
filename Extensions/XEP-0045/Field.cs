using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Xmpp.Extensions
{
    /// <summary>
    /// Represents an xml field element
    /// </summary>
    public class Field
    {
        /// <summary>
        /// The protocol descriptor
        /// </summary>
        public string Var { get; set; }

        /// <summary>
        /// Describes the field data.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Data value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Represents an xml field element
        /// </summary>
        /// <param name="var">Protocol descriptor</param>
        /// <param name="label">Content Description</param>
        /// <param name="value">Data</param>
        public Field(string var, string label, string value)
        {
            Var = string.IsNullOrEmpty(var) ? string.Empty : value;
            Label = string.IsNullOrEmpty(label) ? string.Empty : label;
            Value = string.IsNullOrEmpty(value) ? string.Empty : value;
        }
    }
}
