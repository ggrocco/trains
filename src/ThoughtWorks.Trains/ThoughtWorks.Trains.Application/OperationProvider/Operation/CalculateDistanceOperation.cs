using System.Linq;

namespace ThoughtWorks.Trains.Application.OperationProvider.Operation
{
    using ThoughtWorks.Trains.Domain.Entity;
    using ThoughtWorks.Trains.Application.Extensions;

    internal class CalculateDistanceOperation : BaseRouteOperation
    {
        private readonly string[] routes;

        public CalculateDistanceOperation( string operationIdentifier, string[] routes )
            : base( operationIdentifier )
        {
            this.routes = routes;
        }

        protected override string ProtectedRun( Railroad railroad )
        {
            var towns = routes.Select( railroad.Get ).ToArray();
            var graph = railroad.ToGraph();

            var cost = graph.GetRoute( towns ).Cost;

            return cost.ToString();
        }
    }
}
