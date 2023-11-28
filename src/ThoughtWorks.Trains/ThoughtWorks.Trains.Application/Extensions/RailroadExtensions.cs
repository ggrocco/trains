using System.Linq;

namespace ThoughtWorks.Trains.Application.Extensions
{
    using ThoughtWorks.Lib.Graph;
    using ThoughtWorks.Trains.Domain.Entity;

    internal static class RailroadExtensions
    {
        public static Graph<Town> ToGraph( this Railroad railsroad )
        {
            var graph = new Graph<Town>();

            // Get all towns.
            var towns = railsroad.Routes.Select( r => r.From ).Union( railsroad.Routes.Select( r => r.To ) ).Distinct();
            graph.AddNodes( towns.ToArray() );

            // Incluid in the graph the routes.
            foreach( var route in railsroad.Routes )
                graph.AddEdge( route.From, route.To, route.Distance );

            return graph;
        }
    }
}
