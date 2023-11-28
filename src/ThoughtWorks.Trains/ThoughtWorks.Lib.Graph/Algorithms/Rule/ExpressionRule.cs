using System;

namespace ThoughtWorks.Lib.Graph.Algorithms.Rule
{
    public class ExpressionRule<T_Node> : IRule<T_Node>
            where T_Node : IEquatable<T_Node>
    {
        public ExpressionRule( Func<ListEdge<T_Node>, bool, bool> func, bool alowsConcatenation = false )
        {
            this.Func = func;
            this.AllowsConcatenation = alowsConcatenation;
        }

        private Func<ListEdge<T_Node>, bool, bool> Func
        {
            get;
            set;
        }

        #region IValidator Members

        public bool Applies( ListEdge<T_Node> source, bool isInTarget )
        {
            return Func( source, isInTarget );
        }

        public bool AllowsConcatenation
        {
            get;
            private set;
        }

        #endregion
    }
}
