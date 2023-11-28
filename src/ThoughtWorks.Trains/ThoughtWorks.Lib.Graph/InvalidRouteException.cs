using System;

namespace ThoughtWorks.Lib.Graph
{
    public class InvalidRouteException : Exception
    {
        public InvalidRouteException()
            : base( "NO SUCH ROUTE" )
        {
        }

        public InvalidRouteException( string msg )
            : base( msg )
        {
        }
    }
}
