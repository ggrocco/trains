using System;
using System.Collections.Generic;
using System.Linq;

namespace ThoughtWorks.Lib.Graph
{
    public class Node<T_Node> : IEquatable<T_Node>
        where T_Node : IEquatable<T_Node>
    {

        internal Node( T_Node data )
        {
            this.Data = data;
            this.Edges = new List<Edge<T_Node>>();
        }

        public T_Node Data
        {
            get;
            private set;
        }

        public IList<Edge<T_Node>> Edges
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return this.Data.ToString();
        }

        internal Edge<T_Node> Get( T_Node node )
        {
            var edge = this.Edges.SingleOrDefault( e => e.To.Equals( node ) );
            if( edge == null )
                throw new InvalidRouteException();

            return edge;
        }

        #region IEquatable<T_Node> Members

        public bool Equals( T_Node other )
        {
            return this.Data.Equals( other );
        }

        #endregion
    }
}
