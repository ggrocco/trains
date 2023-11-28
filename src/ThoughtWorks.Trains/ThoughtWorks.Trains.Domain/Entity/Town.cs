using System;

namespace ThoughtWorks.Trains.Domain.Entity
{
    public class Town : IEquatable<Town>
    {
        public Town( string townName )
        {
            this.Name = townName;
        }

        public string Name
        {
            get;
            set;
        }

        #region IEquatable<Town> Members

        public bool Equals( Town other )
        {
            return this.Name.Equals( other.Name );
        }

        #endregion
    }
}
