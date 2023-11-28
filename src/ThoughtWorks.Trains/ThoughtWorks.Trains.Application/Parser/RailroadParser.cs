using System;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace ThoughtWorks.Trains.Application.Parser
{
    using ThoughtWorks.Trains.Domain.Entity;
    using ThoughtWorks.Trains.Application.Extensions;

    internal sealed class RailroadParser : IRailroadParser
    {
        private const string INPUT_PATTERN = "INPUT_PATTERN";
        private const string INPUT_PATTERN_GROUP = "INPUT_PATTERN_GROUP";

        private const string INPUT_GROUP_FROM = "from";
        private const string INPUT_GROUP_TO = "to";
        private const string INPUT_GROUP_DISTANCE = "distance";

        #region Singleton

        private static readonly RailroadParser instance = new RailroadParser();

        private RailroadParser()
        {
        }

        public static RailroadParser Instance
        {
            get
            {
                return instance;
            }
        }

        #endregion

        #region IRailroadParser Members

        public Railroad Parse( string routeRepresentation )
        {
            if( String.IsNullOrEmpty( routeRepresentation ) )
                throw new ArgumentException( "Invalid empty route" );

            // Valid the route representation.
            if( !MakeRegex( INPUT_PATTERN ).IsMatch( routeRepresentation ) )
                throw new ArgumentException( "Invalid input: " + routeRepresentation );

            // Group route based in the pattern.
            var reg = MakeRegex( INPUT_PATTERN_GROUP );
            var matchs = reg.Matches( routeRepresentation );

            var railroad = new Railroad();
            foreach( Match match in matchs )
            {
                var townFrom = GetTown( railroad, match, INPUT_GROUP_FROM );
                var townTo = GetTown( railroad, match, INPUT_GROUP_TO );
                var distance = GetDistance( match, INPUT_GROUP_DISTANCE );

                var route = new Route( townFrom, townTo, distance );
                railroad.Routes.Add( route );
            }

            return railroad;
        }

        #endregion

        private static Town GetTown( Railroad railroad, Match match, string groupName )
        {
            var townName = match.Get( groupName );
            if( !railroad.Towns.Any( t => t.Name.Equals( townName ) ) )
                railroad.Towns.Add( new Town( townName ) );

            return railroad.Towns.Single( t => t.Name.Equals( townName ) );
        }

        private static int GetDistance( Match match, string groupName )
        {
            var value = match.Get( groupName );

            int result;
            if( !Int32.TryParse( value, out result ) )
                throw new InvalidCastException( "The distance is not a integer value." );

            return result;
        }

        private static Regex MakeRegex( string pattern )
        {
            try
            {
                var reg = new Regex( GetPattern( pattern ) );
                return reg;
            }
            catch( ArgumentException e )
            {
                throw new ConfigurationErrorsException( "Invalid RegEx in " + pattern, e );
            }
        }

        private static string GetPattern( string pattern )
        {
            var input = ConfigurationManager.AppSettings[pattern];
            if( input == null )
                throw new ConfigurationErrorsException( "Invalid " + pattern + " definition in configuration file." );

            return input;
        }
    }
}
