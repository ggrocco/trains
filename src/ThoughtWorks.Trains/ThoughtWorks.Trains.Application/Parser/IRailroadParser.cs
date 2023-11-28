namespace ThoughtWorks.Trains.Application.Parser
{
    using ThoughtWorks.Trains.Domain.Entity;

    public interface IRailroadParser
    {
        Railroad Parse( string routeRepresentation );
    }
}
