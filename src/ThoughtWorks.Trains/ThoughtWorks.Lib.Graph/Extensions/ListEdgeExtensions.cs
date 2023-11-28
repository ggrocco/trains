using System;
using System.Collections.Generic;
using System.Linq;

namespace ThoughtWorks.Lib.Graph.Extensions
{
    public static class ListEdgeExtensions
    {
        public static ListEdge<T_Node> BestRoute<T_Node>( this IEnumerable<ListEdge<T_Node>> routes )
            where T_Node : IEquatable<T_Node>
        {
            return routes.OrderBy( r => r.Cost ).ThenBy( r => r.Stops ).First();
        }

        public static bool Empty<T_Node>( this IEnumerable<ListEdge<T_Node>> routes )
            where T_Node : IEquatable<T_Node>
        {
            return !routes.Any();
        }
    }
}
