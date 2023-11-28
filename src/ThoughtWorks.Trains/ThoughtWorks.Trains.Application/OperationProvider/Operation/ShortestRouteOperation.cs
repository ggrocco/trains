namespace ThoughtWorks.Trains.Application.OperationProvider.Operation
{
    using ThoughtWorks.Lib.Graph.Algorithms;
    using ThoughtWorks.Trains.Domain.Entity;
    using ThoughtWorks.Trains.Application.Extensions;

    internal class ShortestRouteOperation : BaseRouteOperation
    {
        private readonly string sourceName;
        private readonly string targetName;

        public ShortestRouteOperation( string operationIdentifier, string sourceName, string targetName )
            : base( operationIdentifier )
        {
            this.sourceName = sourceName;
            this.targetName = targetName;
        }

        protected override string ProtectedRun( Railroad railroad )
        {
            var graph = railroad.ToGraph();
            var townSource = railroad.Get( this.sourceName );
            var townTarge = railroad.Get( this.targetName );

            // Calculate the best route.
            var totalRoutes = Explorador.BestRoute( graph, townSource, townTarge ).Cost;

            return totalRoutes.ToString();
        }
    }
}
