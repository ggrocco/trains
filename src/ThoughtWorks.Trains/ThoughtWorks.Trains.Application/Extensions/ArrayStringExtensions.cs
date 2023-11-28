using System;
using System.Linq;

namespace ThoughtWorks.Trains.Application.Extensions
{
    public static class ArrayStringExtensions
    {
        public static bool Empty( this string[] array )
        {
            return ( array.Count() == 0 ) || ( array.Count() == 1 && String.IsNullOrEmpty( array[0] ) );
        }
    }
}
