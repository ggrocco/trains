using System;
using System.Collections.Generic;
using System.Linq;

namespace ThoughtWorks.Lib.Graph
{
    public class ListEdge<T_Node> : List<Edge<T_Node>>
        where T_Node : IEquatable<T_Node>
    {
        private ListEdge( IEnumerable<Edge<T_Node>> list )
            : base( list )
        {
        }

        internal ListEdge( Node<T_Node> from )
        {
            this.Add( new Edge<T_Node>( from, 0 ) );
        }

        internal ListEdge( Node<T_Node> from, Edge<T_Node> egde )
            : this( from )
        {
            this.Add( egde );
        }

        public Node<T_Node> From
        {
            get
            {
                return this.First().To;
            }
        }

        public Node<T_Node> To
        {
            get
            {
                return this.Last().To;
            }
        }

        public IList<Edge<T_Node>> NextEdges
        {
            get
            {
                return this.Last().To.Edges;
            }
        }

        public int Stops
        {
            get
            {
                return this.Count - 1;
            }
        }

        public decimal Cost
        {
            get
            {
                return this.Sum( e => e.Cost );
            }
        }

        // the same idea of ICloneable.
        internal ListEdge<T_Node> CloneAndAppend( Edge<T_Node> edge )
        {
            return new ListEdge<T_Node>( this ) { edge };
        }
    }
}
