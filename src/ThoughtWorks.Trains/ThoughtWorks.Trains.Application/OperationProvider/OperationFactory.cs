using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ThoughtWorks.Trains.Application.OperationProvider
{
    using ThoughtWorks.Lib.Graph.Algorithms.Rule;
    using ThoughtWorks.Trains.Domain.Entity;
    using ThoughtWorks.Trains.Application.Extensions;
    using ThoughtWorks.Trains.Application.OperationProvider.Rules;
    using ThoughtWorks.Trains.Application.OperationProvider.Operation;

    internal class OperationFactory
    {
        // Regex of dsl.
        private static readonly Regex CALCULATE_DISTANCE_OPERATION = new Regex( @"(?<index>\d+)\. The distance of the route (?<routes>(\w(-?))+)\." );
        private static readonly Regex NUMBER_OF_TRIPS_WITH_STOPS = new Regex( @"(?<index>\d+)\. The number of trips starting at (?<from>\w) and ending at (?<to>\w) with (?<rule>a maximum of|exactly) (?<stops>\d+) stops\." );
        private static readonly Regex SHORTEST_ROUTE = new Regex( @"(?<index>\d+)\. The length of the shortest route \(in terms of distance to travel\) from (?<from>\w) to (?<to>\w)\." );
        private static readonly Regex NUMBER_DIFFERENT_ROUTES = new Regex( @"(?<index>\d+)\. The number of different routes from (?<from>\w) to (?<to>\w) with a distance of less than (?<distance>\d+)\." );

        // Linked the dsl with operation.
        private static readonly IDictionary<Regex, Func<string, BaseRouteOperation>> DSL_TO_OPERATION = new Dictionary<Regex, Func<string, BaseRouteOperation>>(){
            {CALCULATE_DISTANCE_OPERATION, dsl => CreateCalculateDistanceOperation(CALCULATE_DISTANCE_OPERATION.Match(dsl))},
            {NUMBER_OF_TRIPS_WITH_STOPS, dsl => CreateNumberOfTripsWithStops(NUMBER_OF_TRIPS_WITH_STOPS.Match(dsl))},
            {SHORTEST_ROUTE, dsl => CreateShortestRoute(SHORTEST_ROUTE.Match(dsl))},
            {NUMBER_DIFFERENT_ROUTES, dsl => CreateDifferentRoutes(NUMBER_DIFFERENT_ROUTES.Match(dsl))},
        };

        public BaseRouteOperation CreateFromDsl( string dsl )
        {
            var key = DSL_TO_OPERATION.Keys.SingleOrDefault( k => k.IsMatch( dsl ) );
            if( key == null )
                throw new ArgumentException( "No representation is valid for entry -> " + dsl );

            return DSL_TO_OPERATION[key]( dsl );
        }

        private static CalculateDistanceOperation CreateCalculateDistanceOperation( Match matchs )
        {
            var routes = matchs.Get( "routes" ).Split( '-' );

            var operation = new CalculateDistanceOperation( matchs.Get( "index" ), routes );
            return operation;
        }

        private static RoutesBasedRuleOperation CreateNumberOfTripsWithStops( Match matchs )
        {
            var stops = Convert.ToInt32( matchs.Get( "stops" ) );

            IRule<Town> rule;
            if( "exactly".Equals( matchs.Get( "rule" ) ) )
                rule = new ExactlyStopsRule( stops );
            else
                rule = new MaxStopRule( stops );

            var operation = new RoutesBasedRuleOperation( matchs.Get( "index" ), matchs.Get( "from" ), matchs.Get( "to" ), rule );
            return operation;
        }

        private static ShortestRouteOperation CreateShortestRoute( Match matchs )
        {
            var operation = new ShortestRouteOperation( matchs.Get( "index" ), matchs.Get( "from" ), matchs.Get( "to" ) );
            return operation;
        }

        private static RoutesBasedRuleOperation CreateDifferentRoutes( Match matchs )
        {
            var distance = Convert.ToInt32( matchs.Get( "distance" ) );
            var rule = new DistanceOfLessThanRule( distance );
            var operation = new RoutesBasedRuleOperation( matchs.Get( "index" ), matchs.Get( "from" ), matchs.Get( "to" ), rule );
            return operation;
        }
    }
}
