namespace ThoughtWorks.Trains.Application.OperationProvider.Operation
{
    using ThoughtWorks.Lib.Graph.Algorithms;
    using ThoughtWorks.Lib.Graph.Algorithms.Rule;
    using ThoughtWorks.Trains.Domain.Entity;
    using ThoughtWorks.Trains.Application.Extensions;

    internal class RoutesBasedRuleOperation : BaseRouteOperation
    {
        private readonly string sourceName;
        private readonly string targetName;
        private readonly IRule<Town> rule;

        public RoutesBasedRuleOperation( string operationIdentifier, string sourceName, string targetName, IRule<Town> rule )
            : base( operationIdentifier )
        {
            this.sourceName = sourceName;
            this.targetName = targetName;
            this.rule = rule;
        }

        protected override string ProtectedRun( Railroad railroad )
        {
            var graph = railroad.ToGraph();
            var townSource = railroad.Get( this.sourceName );
            var townTarge = railroad.Get( this.targetName );

            // Calculate the all possibles routes.
            var totalRoutes = Explorador.AllRoutes( graph, townSource, townTarge, this.rule ).Count;
            return totalRoutes.ToString();
        }
    }
}
