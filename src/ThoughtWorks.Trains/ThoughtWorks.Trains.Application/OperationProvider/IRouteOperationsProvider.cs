using System.Collections.Generic;

namespace ThoughtWorks.Trains.Application.OperationProvider
{
    public interface IRouteOperationsProvider
    {
        IEnumerable<BaseRouteOperation> ListAllOperations( string[] dslRepresentations );
    }
}
