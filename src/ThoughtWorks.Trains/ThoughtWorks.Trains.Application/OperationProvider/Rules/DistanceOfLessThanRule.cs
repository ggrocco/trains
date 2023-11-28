namespace ThoughtWorks.Trains.Application.OperationProvider.Rules
{
    using ThoughtWorks.Lib.Graph.Algorithms.Rule;
    using ThoughtWorks.Trains.Domain.Entity;

    internal class DistanceOfLessThanRule : ExpressionRule<Town>
    {
        public DistanceOfLessThanRule( int distance )
            : base( ( source, isInTarget ) => source.Cost < distance, true )
        {
        }
    }
}
