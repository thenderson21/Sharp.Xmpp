using System;

namespace Sharp.Xmpp.Extensions
{
    [Serializable]
    internal class Feature
    {
        public string Var
        {
            get;
            private set;
        }

        public Feature(string var)
        {
            this.Var = var;
        }
    }
}
