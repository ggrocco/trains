using System;
using ThoughtWorks.Lib.Graph;

namespace ThoughtWorks.Trains.Application.OperationProvider
{
    using ThoughtWorks.Trains.Domain.Entity;

    public abstract class BaseRouteOperation
    {
        protected BaseRouteOperation( string operationIdentifier )
        {
            this.OperationIdentifier = operationIdentifier;
        }

        public void Run( Railroad railroad, RouteOperationResult output )
        {
            string processResult;

            try
            {
                processResult = ProtectedRun( railroad );
            }
            catch( InvalidRouteException err )
            {
                processResult = err.Message;
            }
            catch( ArgumentException err )
            {
                processResult = err.Message;
            }

            output.Append( this.OperationIdentifier, processResult );
        }

        protected abstract string ProtectedRun( Railroad railroad );

        private string OperationIdentifier
        {
            get;
            set;
        }
    }
}
