using System;

namespace ThoughtWorks.Lib.Graph.Algorithms.Rule
{
    public class DefaultRule<T_Node> : IRule<T_Node>
            where T_Node : IEquatable<T_Node>
    {
        public DefaultRule()
        {
            this.AllowsConcatenation = false;
        }

        #region IValidator Members

        public bool Applies( ListEdge<T_Node> source, bool isInTarget )
        {
            return true;
        }

        public bool AllowsConcatenation
        {
            get;
            private set;
        }

        #endregion
    }
}
