using System.Text;

namespace ThoughtWorks.Trains.Application.OperationProvider
{
    public class RouteOperationResult
    {
        private readonly StringBuilder buffer;

        public RouteOperationResult()
        {
            this.buffer = new StringBuilder();
        }

        internal void Append( string index, string msg )
        {
            if( buffer.Length > 0 )
                buffer.Append( "\n" );
            buffer.AppendFormat( "Output #{0}: {1}", index, msg );
        }

        public override string ToString()
        {
            return this.buffer.ToString();
        }
    }
}
