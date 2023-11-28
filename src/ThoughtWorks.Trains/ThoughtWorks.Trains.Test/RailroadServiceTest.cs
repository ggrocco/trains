using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ThoughtWorks.Trains.Test
{
    using ThoughtWorks.Trains.Application;

    /// <summary>
    /// Summary description for RailroadServiceTest
    /// </summary>
    [TestClass]
    public class RailroadServiceTest
    {
        private readonly RailroadService railroadService;

        public RailroadServiceTest()
        {
            this.railroadService = RailroadService.Instance;
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void Try_empty_Route()
        {
            this.railroadService.RunRouteOperations( "", new[] { "2. The distance of the route A-D." } );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void Try_invalid_Route()
        {
            this.railroadService.RunRouteOperations( "AF5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7", new[] { "2. The distance of the route A-D." } );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void Try_empty_dls()
        {
            this.railroadService.RunRouteOperations( "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7", new string[0] );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void Try_invalid_dsl()
        {
            this.railroadService.RunRouteOperations( "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7", new[] { "99. Invalid dsl to distance route A-D." } );
        }

        [TestMethod]
        public void Try_impossible_route_to_shortest_route()
        {
            var output = this.railroadService.RunRouteOperations( "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7", new[] { "99. The length of the shortest route (in terms of distance to travel) from A to A." } );
            Assert.AreEqual( "Output #99: NO SUCH ROUTE", output );
        }

        [TestMethod]
        public void Try_impossible_route_to_distance()
        {
            var output = this.railroadService.RunRouteOperations( "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7", new[] { "5. The distance of the route A-E-D." } );
            Assert.AreEqual( "Output #5: NO SUCH ROUTE", output );
        }

        [TestMethod]
        public void Distance_of_route_A_D()
        {
            var output = this.railroadService.RunRouteOperations( "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7", new[] { "2. The distance of the route A-D." } );
            Assert.AreEqual( "Output #2: 5", output );
        }

        [TestMethod]
        public void Distance_of_route_A_D_C()
        {
            var output = this.railroadService.RunRouteOperations( "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7", new[] { "3. The distance of the route A-D-C." } );
            Assert.AreEqual( "Output #3: 13", output );
        }
    }
}
