using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ThoughtWorks.Trains.Test
{
    using ThoughtWorks.Trains.Application.Parser;

    [TestClass]
    public class RouteParseTest
    {
        [TestMethod]
        public void Validade_the_route()
        {
            var parser = RailroadParser.Instance;
            var result = parser.Parse( "AB5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7" );

            Assert.AreEqual( 9, result.Routes.Count() );
            Assert.AreEqual( 5, result.Towns.Count() );

            var routes = result.Routes;
            Assert.AreEqual( "A", routes.First().From.Name );
            Assert.AreEqual( "B", routes.First().To.Name );
            Assert.AreEqual( 5, routes.First().Distance );

            Assert.AreEqual( "A", routes.Last().From.Name );
            Assert.AreEqual( "E", routes.Last().To.Name );
            Assert.AreEqual( 7, routes.Last().Distance );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void Try_route_rnvalid()
        {
            var parser = RailroadParser.Instance;
            parser.Parse( "AA5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7" );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void Try_invalid_input()
        {
            var parser = RailroadParser.Instance;
            parser.Parse( "AF5, BC4, CD8, DC8, DE6, AD5, CE2, EB3, AE7" );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void Try_rmpty_route()
        {
            var parser = RailroadParser.Instance;
            parser.Parse( "" );
        }
    }
}
