namespace ThoughtWorks.Trains.Application.OperationProvider.Rules
{
    using ThoughtWorks.Lib.Graph.Algorithms.Rule;
    using ThoughtWorks.Trains.Domain.Entity;

    internal class ExactlyStopsRule : ExpressionRule<Town>
    {
        public ExactlyStopsRule( int stops )
            : base( ( source, isInTarget ) =>
                isInTarget ? 
                    source.Stops == stops : 
                    source.Stops < stops
            , true )
        {
        }
    }
}
