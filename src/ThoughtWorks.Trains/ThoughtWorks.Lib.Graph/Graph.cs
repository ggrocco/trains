using System;
using System.Collections.Generic;
using System.Linq;

namespace ThoughtWorks.Lib.Graph
{
    public class Graph<T_Node>
        where T_Node : IEquatable<T_Node>
    {
        public Graph()
        {
           this.Nodes = new List<Node<T_Node>>();
        }

        public IList<Node<T_Node>> Nodes
        {
            get;
            private set;
        }

        public void AddNodes( params T_Node[] nodes )
        {
            foreach( var node in nodes )
                this.Nodes.Add( new Node<T_Node>( node ) );
        }

        public Graph<T_Node> AddEdge( T_Node from, T_Node to, decimal cost )
        {
            this.Get( from ).Edges.Add( new Edge<T_Node>( this.Get( to ), cost ) );
            return this;
        }

        public Node<T_Node> Get( T_Node nodeData )
        {
            var node = this.Nodes.SingleOrDefault( n => n.Equals( nodeData ) );
            if( node == null )
                throw new InvalidRouteException();

            return node;
        }

        public ListEdge<T_Node> GetRoute( params T_Node[] nodes )
        {
            if( nodes.Count() < 2 )
                throw new ArgumentException("");
            
            if( nodes.Count() == 2 && nodes.First().Equals( nodes.Last() ) )
                throw new ArgumentException("");

            var result = new ListEdge<T_Node>( this.Get( nodes.First() ) );

            for (var i = 1; i < nodes.Length; i++)
            {
                var nextEdge = result.Last().To.Get( nodes[i] );
                result.Add( nextEdge );
            }

            return result;
        }
    }
}