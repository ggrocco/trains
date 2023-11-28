using System;
using System.Collections.Generic;
using System.Linq;

namespace ThoughtWorks.Trains.Domain.Entity
{
    public class Railroad
    {
        public Railroad()
        {
            this.Towns = new List<Town>();
            this.Routes = new List<Route>();
        }

        public IList<Town> Towns
        {
            get;
            private set;
        }

        public IList<Route> Routes
        {
            get;
            private set;
        }

        public Town Get( string sourceName )
        {
            var town = this.Towns.SingleOrDefault( t => sourceName.Equals( t.Name ) );
            if( town == null )
                throw new ArgumentException( "Invalid town name " + sourceName );

            return town;
        }
    }
}
