using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ThoughtWorks.Trains.Test
{
    using ThoughtWorks.Trains.Domain.Entity;
    using ThoughtWorks.Trains.Application.Extensions;

    [TestClass]
    public class RailroadTest
    {
        [TestMethod]
        public void Convert_railroad_to_graph()
        {
            var townA = new Town( "A" );
            var townB = new Town( "B" );
            var townC = new Town( "C" );

            var railroad = new Railroad();
            railroad.Routes.Add( new Route( townA, townB, 3 ) );
            railroad.Routes.Add( new Route( townB, townC, 4 ) );
            railroad.Routes.Add( new Route( townC, townA, 5 ) );
            var graph = railroad.ToGraph();

            Assert.AreEqual( townA, graph.Nodes[0].Data );
            Assert.AreEqual( townB, graph.Nodes[0].Edges.First().To.Data );
            Assert.AreEqual( 3, graph.Nodes[0].Edges.First().Cost );

            Assert.AreEqual( townB, graph.Nodes[1].Data );
            Assert.AreEqual( townC, graph.Nodes[1].Edges.First().To.Data );
            Assert.AreEqual( 4, graph.Nodes[1].Edges.First().Cost );

            Assert.AreEqual( townC, graph.Nodes[2].Data );
            Assert.AreEqual( townA, graph.Nodes[2].Edges.First().To.Data );
            Assert.AreEqual( 5, graph.Nodes[2].Edges.First().Cost );
        }

        [TestMethod]
        public void Convert_empty_railroad_to_graph()
        {
            var railroad = new Railroad();
            var graph = railroad.ToGraph();

            Assert.AreEqual( 0, graph.Nodes.Count );
        }
    }
}
