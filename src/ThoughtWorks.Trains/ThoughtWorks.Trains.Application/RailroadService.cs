namespace ThoughtWorks.Trains.Application
{
    using ThoughtWorks.Trains.Application.Extensions;
    using ThoughtWorks.Trains.Application.OperationProvider;
    using ThoughtWorks.Trains.Application.Parser;

    public sealed class RailroadService
    {
         #region Singleton

        private static readonly RailroadService instance = new RailroadService();

        private RailroadService()
        {
        }

        public static RailroadService Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion

        public string RunRouteOperations( string routeRepresentation, string[] dslRepresentations )
        {
            // Parse text representation to the railroad entities.
            var railroad = GeRailroadParser().Parse( routeRepresentation );

            // Get the defines operations over dsl.
            var operations = GetRouteOperationProvider().ListAllOperations( dslRepresentations );

            // Apply the operations on the graph.
            var result = operations.Process( railroad );

            // Return the text output based on the result of the operations.
            return result.ToString();
        }

        private static IRouteOperationsProvider GetRouteOperationProvider()
        {
            return RouteOperationsProvider.Instance;
        }

        private static IRailroadParser GeRailroadParser()
        {
            return RailroadParser.Instance;
        }
    }
}
