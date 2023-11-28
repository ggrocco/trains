using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ThoughtWorks.Lib.Graph.Test
{
    using ThoughtWorks.Lib.Graph.Algorithms;
    using ThoughtWorks.Lib.Graph.Algorithms.Rule;

    [TestClass]
    public class GraphTest
    {
        private readonly Graph<string> graph;

        public GraphTest()
        {
            this.graph = new Graph<string>();
            this.graph.AddNodes( "A", "B", "C", "D", "E" );
            this.graph.AddEdge( "A", "B", 5 ).AddEdge( "B", "C", 4 ).AddEdge( "C", "D", 8 ).AddEdge( "D", "C", 8 ).AddEdge( "D", "E", 6 );
            this.graph.AddEdge( "A", "D", 5 ).AddEdge( "C", "E", 2 ).AddEdge( "E", "B", 3 ).AddEdge( "A", "E", 7 );
        }

        [TestMethod]
        [ExpectedException( typeof( InvalidRouteException ) )]
        public void Try_Empty_Graph()
        {
            new Graph<string>().GetRoute( "A", "B", "C" );
        }

        [TestMethod]
        public void Weight_Route_A_B_C()
        {

            Assert.AreEqual( 9, graph.GetRoute( "A", "B", "C" ).Cost );
        }

        [TestMethod]
        public void Weight_Route_A_D()
        {
            Assert.AreEqual( 5, graph.GetRoute( "A", "D" ).Cost );
        }

        [TestMethod]
        public void Weight_Route_A_D_C()
        {
            Assert.AreEqual( 13, graph.GetRoute( "A", "D", "C" ).Cost );
        }

        [TestMethod]
        public void Weight_Route_A_E_B_C_D()
        {
            Assert.AreEqual( 22, graph.GetRoute( "A", "E", "B", "C", "D" ).Cost );
        }

        [TestMethod]
        [ExpectedException( typeof( InvalidRouteException ) )]
        public void Weight_Route_A_E_D()
        {
            graph.GetRoute( "A", "E", "D" );
        }

        [TestMethod]
        public void Number_trips_starting_at_C_and_C_with_max_3_stops()
        {
            Assert.AreEqual( 2, Explorador.AllRoutes( graph, "C", "C",
                new ExpressionRule<string>( ( source, math ) => source.Stops <= 3, true ) ).Count
                );
        }

        [TestMethod]
        public void Number_trips_starting_at_A_and_C_with_exactly__4stops()
        {
            Assert.AreEqual( 3, Explorador.AllRoutes( graph, "A", "C",
               new ExpressionRule<string>( ( source, math ) => math ? source.Stops == 4 : source.Stops < 4, true ) ).Count
               );
        }

        [TestMethod]
        public void Shortest_route_from_A_to_C()
        {
            Assert.AreEqual( 9, Explorador.BestRoute( graph, "A", "C" ).Cost );
        }

        [TestMethod]
        public void Shortest_route_from_B_to_B()
        {
            Assert.AreEqual( 9, Explorador.BestRoute( graph, "B", "B" ).Cost );
        }

        [TestMethod]
        public void Diferente_routes_from_C_to_C_less_than_30()
        {
            var routesCforC = Explorador.AllRoutes( graph, "C", "C",
                new ExpressionRule<string>( ( source, math ) => source.Cost < 30, true ) );
            Assert.AreEqual( 7, routesCforC.Count );
        }
    }
}
