using System.Collections.Generic;

namespace ThoughtWorks.Trains.Application.Extensions
{
    using ThoughtWorks.Trains.Domain.Entity;
    using ThoughtWorks.Trains.Application.OperationProvider;

    internal static class OperationExtencions
    {
        public static RouteOperationResult Process( this IEnumerable<BaseRouteOperation> operations, Railroad railroad )
        {
            var output = new RouteOperationResult();
            foreach( var operation in operations )
            {
                operation.Run( railroad, output );
            }
            return output;
        }
    }
}
