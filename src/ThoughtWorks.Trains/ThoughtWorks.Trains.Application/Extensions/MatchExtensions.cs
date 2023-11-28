using System;
using System.Text.RegularExpressions;

namespace ThoughtWorks.Trains.Application.Extensions
{
    internal static class MatchExtensions
    {
        public static string Get( this Match match, string groupName )
        {
            var group = match.Groups[groupName];
            if( group == null )
                throw new ArgumentException( "Not found in the match a group named '" + groupName + "'" );

            return group.Value;
        }
    }
}
