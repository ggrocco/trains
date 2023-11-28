using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ThoughtWorks.Trains.Test
{
    using ThoughtWorks.Trains.Domain.Entity;
    using ThoughtWorks.Trains.Application.Parser;
    using ThoughtWorks.Trains.Application.OperationProvider;
    using ThoughtWorks.Trains.Application.OperationProvider.Operation;

    [TestClass]
    public class OperationsFactoryTest
    {
        private readonly Railroad railroad;
        private readonly OperationFactory factory;

        public OperationsFactoryTest()
        {
            this.railroad = RailroadParser.Instance.Parse( "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7" );
            this.factory = new OperationFactory();
        }

        [TestMethod]
        public void The_distance_of_the_route_A_B_C()
        {
            var operation = this.factory.CreateFromDsl( "1. The distance of the route A-B-C." );
            var output = new RouteOperationResult();
            operation.Run( railroad, output );

            Assert.AreEqual( typeof( CalculateDistanceOperation ), operation.GetType() );
            Assert.AreEqual( "Output #1: 9", output.ToString() );
        }

        [TestMethod]
        public void The_distance_of_the_route_A_D()
        {
            var operation = this.factory.CreateFromDsl( "2. The distance of the route A-D." );
            var output = new RouteOperationResult();
            operation.Run( railroad, output );

            Assert.AreEqual( typeof( CalculateDistanceOperation ), operation.GetType() );
            Assert.AreEqual( "Output #2: 5", output.ToString() );
        }

        [TestMethod]
        public void The_distance_of_the_route_A_D_C()
        {
            var operation = this.factory.CreateFromDsl( "3. The distance of the route A-D-C." );
            var output = new RouteOperationResult();
            operation.Run( railroad, output );

            Assert.AreEqual( typeof( CalculateDistanceOperation ), operation.GetType() );
            Assert.AreEqual( "Output #3: 13", output.ToString() );
        }

        [TestMethod]
        public void The_distance_of_the_route_A_E_B_C_D()
        {
            var operation = this.factory.CreateFromDsl( "4. The distance of the route A-E-B-C-D." );
            var output = new RouteOperationResult();
            operation.Run( railroad, output );

            Assert.AreEqual( typeof( CalculateDistanceOperation ), operation.GetType() );
            Assert.AreEqual( "Output #4: 22", output.ToString() );
        }

        [TestMethod]
        public void The_distance_of_the_route_A_E_D()
        {
            var operation = this.factory.CreateFromDsl( "5. The distance of the route A-E-D." );
            var output = new RouteOperationResult();
            operation.Run( railroad, output );

            Assert.AreEqual( typeof( CalculateDistanceOperation ), operation.GetType() );
            Assert.AreEqual( "Output #5: NO SUCH ROUTE", output.ToString() );
        }

        [TestMethod]
        public void Number_of_trips_stating_C_and_anding_at_C_with_a_maximum_of_3_stops()
        {
            var operation = this.factory.CreateFromDsl( "6. The number of trips starting at C and ending at C with a maximum of 3 stops." );
            var output = new RouteOperationResult();
            operation.Run( railroad, output );

            Assert.AreEqual( typeof( RoutesBasedRuleOperation ), operation.GetType() );
            Assert.AreEqual( "Output #6: 2", output.ToString() );
        }

        [TestMethod]
        public void Number_of_trips_stating_C_and_anding_at_C_with_exactly_4_stops()
        {
            var operation = factory.CreateFromDsl( "7. The number of trips starting at A and ending at C with exactly 4 stops." );
            var output = new RouteOperationResult();
            operation.Run( railroad, output );

            Assert.AreEqual( typeof( RoutesBasedRuleOperation ), operation.GetType() );
            Assert.AreEqual( "Output #7: 3", output.ToString() );
        }

        [TestMethod]
        public void The_length_of_the_shortest_route_from_A_to_C()
        {
            var operation = factory.CreateFromDsl( "8. The length of the shortest route (in terms of distance to travel) from A to C." );
            var output = new RouteOperationResult();
            operation.Run( railroad, output );

            Assert.AreEqual( typeof( ShortestRouteOperation ), operation.GetType() );
            Assert.AreEqual( "Output #8: 9", output.ToString() );
        }

        [TestMethod]
        public void The_length_of_the_shortest_route_from_B_to_B()
        {
            var operation = factory.CreateFromDsl( "9. The length of the shortest route (in terms of distance to travel) from B to B." );
            var output = new RouteOperationResult();
            operation.Run( railroad, output );

            Assert.AreEqual( typeof( ShortestRouteOperation ), operation.GetType() );
            Assert.AreEqual( "Output #9: 9", output.ToString() );
        }

        [TestMethod]
        public void The_number_of_different_routes_from_C_to_C()
        {
            var operation = factory.CreateFromDsl( "10. The number of different routes from C to C with a distance of less than 30." );
            var output = new RouteOperationResult();
            operation.Run( railroad, output );

            Assert.AreEqual( typeof( RoutesBasedRuleOperation ), operation.GetType() );
            Assert.AreEqual( "Output #10: 7", output.ToString() );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void Try_Invalid_Dsl_to_Factory()
        {
            factory.CreateFromDsl( "I am invalid dsl!" );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void Try_Empty_Dsl_to_Factory()
        {
            factory.CreateFromDsl( "" );
        }
    }
}
