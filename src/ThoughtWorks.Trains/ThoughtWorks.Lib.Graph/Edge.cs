using System;

namespace ThoughtWorks.Lib.Graph
{
    public class Edge<T_Node> : IEquatable<T_Node>
        where T_Node : IEquatable<T_Node>
    {
        internal Edge( Node<T_Node> to, decimal cost )
        {
            this.To = to;
            this.Cost = cost;
        }

        public decimal Cost
        {
            get;
            private set;
        }

        public Node<T_Node> To
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return this.To + " -> " + this.Cost;
        }

        #region IEquatable<T_Node> Members

        public bool Equals( T_Node other )
        {
            return this.To.Equals( other );
        }

        #endregion
    }
}
