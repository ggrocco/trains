using System;

namespace ThoughtWorks.Trains.Domain.Entity
{
    public class Route
    {
        public Route( Town from, Town to, int distance )
        {
            if( from.Equals( to ) )
                throw new ArgumentException( "Invalid routes where origin (" + from.Name + ") is equal to the target (" + to.Name + ")." );

            this.From = from;
            this.To = to;
            this.Distance = distance;
        }

        public Town To
        {
            get;
            set;
        }

        public Town From
        {
            get;
            set;
        }

        public int Distance
        {
            get;
            set;
        }
    }
}
