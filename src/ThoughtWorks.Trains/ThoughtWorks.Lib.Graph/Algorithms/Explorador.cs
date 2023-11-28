using System;
using System.Collections.Generic;
using System.Linq;

namespace ThoughtWorks.Lib.Graph.Algorithms
{
    using ThoughtWorks.Lib.Graph.Algorithms.Rule;
    using ThoughtWorks.Lib.Graph.Extensions;

    public static class Explorador
    {
        public static ListEdge<T_Node> BestRoute<T_Node>( Graph<T_Node> graph, T_Node from, T_Node to )
            where T_Node : IEquatable<T_Node>
        {
            return AllRoutes( graph, from, to ).BestRoute();
        }

        public static List<ListEdge<T_Node>> AllRoutes<T_Node>( Graph<T_Node> graph, T_Node from, T_Node to )
            where T_Node : IEquatable<T_Node>
        {
            return AllRoutes( graph, from, to, new DefaultRule<T_Node>() );
        }

        public static List<ListEdge<T_Node>> AllRoutes<T_Node>( Graph<T_Node> graph, T_Node from, T_Node to, IRule<T_Node> rule )
            where T_Node : IEquatable<T_Node>
        {
            var resultList = new List<ListEdge<T_Node>>();

            var source = graph.Get( from );
            var target = graph.Get( to );
            // prepar the routes.
            var routes = source.Edges.Select( egde => new ListEdge<T_Node>( source, egde ) );
            foreach( var currentRoute in routes )
            {
                Walk( resultList, currentRoute, target, rule ); // Start to wall in the egdes.
            }

            if( resultList.Empty() )
                throw new InvalidRouteException();

            return resultList;
        }

        private static void Walk<T_Node>( ICollection<ListEdge<T_Node>> resultList, ListEdge<T_Node> currentRoute, Node<T_Node> target, IRule<T_Node> rule )
            where T_Node : IEquatable<T_Node>
        {
            if( target.Equals( currentRoute.To ) && rule.Applies( currentRoute, true ) )
            {
                resultList.Add( currentRoute );
                if( !rule.AllowsConcatenation )
                    return;
            }

            if( !rule.Applies( currentRoute, false ) )
                return;

            // Filter if allow or not concatenations.
            var possiblesEdges = ( rule.AllowsConcatenation ) ? currentRoute.NextEdges : currentRoute.NextEdges.Where( edge => !currentRoute.Contains( edge ) );

            // for all edge create a new clone of origin source.
            foreach( var clone in possiblesEdges.Select( currentRoute.CloneAndAppend ) )
            {
                Walk( resultList, clone, target, rule );
            }
        }
    }
}
