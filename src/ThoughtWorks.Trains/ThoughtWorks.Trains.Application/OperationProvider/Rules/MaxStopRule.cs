namespace ThoughtWorks.Trains.Application.OperationProvider.Rules
{
    using ThoughtWorks.Lib.Graph.Algorithms.Rule;
    using ThoughtWorks.Trains.Domain.Entity;

    internal class MaxStopRule : ExpressionRule<Town>
    {
        public MaxStopRule( int stops )
            : base( ( source, isInTarget ) =>
                    source.Stops <= stops
            , true )
        {
        }
    }
}
