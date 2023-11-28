using System;
using System.Collections.Generic;
using System.Linq;

namespace ThoughtWorks.Trains.Application.OperationProvider
{
    using ThoughtWorks.Trains.Application.Extensions;

    internal sealed class RouteOperationsProvider : IRouteOperationsProvider
    {
        #region Singleton

        private static readonly RouteOperationsProvider instance = new RouteOperationsProvider();

        private RouteOperationsProvider()
        {
            this.Factory = new OperationFactory();
        }

        public static RouteOperationsProvider Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion

        public OperationFactory Factory
        {
            get;
            set;
        }

        #region IRouteOperationsProvider Members

        public IEnumerable<BaseRouteOperation> ListAllOperations( string[] dslRepresentations )
        {
            if( dslRepresentations.Empty() )
                throw new ArgumentException( "Invalid empty dsl" );

            return dslRepresentations.Select( line => Factory.CreateFromDsl( line ) );
        }

        #endregion
    }
}
