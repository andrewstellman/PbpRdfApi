using System;
using System.Collections.Generic;
using InCube.Core.Functional;

namespace PbpRdfApi
{
    /// <summary>
    /// Represents a game.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Identifier of the home team.
        /// </summary>
        public string HomeTeamIri { get; private set; }

        /// <summary>
        /// Identifier of the away team.
        /// </summary>
        /// <value>The away team iri.</value>
        public string AwayTeamIri { get; private set; }

        /// <summary>
        /// Location where the game was played.
        /// </summary>
        public Option<string> GameLocation { get; private set; }

        /// <summary>
        /// Time the game was played.
        /// </summary>
        public Option<DateTimeOffset> GameTime { get; private set; }

        /// <summary>
        /// Roster of home team player identifiers.
        /// </summary>
        /// <value>Dictionary of player IRIs to labels</value>
        public IDictionary<string, string> HomeTeamRoster { get; private set; }

        /// <summary>
        /// Roster of away team player identifiers.
        /// </summary>
        /// <value>Dictionary of player IRIs to labels</value>
        public IDictionary<string, string> AwayTeamRoster { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PbpRdfApi.Game"/> class.
        /// </summary>
        public Game(
            string homeTeamIri,
            string awayTeamIri,
            Option<string> gameLocation,
            Option<DateTimeOffset> gameTime,
            IDictionary<string, string> homeTeamRoster,
            IDictionary<string, string> awayTeamRoster)
        {
            HomeTeamIri = homeTeamIri;
            AwayTeamIri = awayTeamIri;
            GameLocation = gameLocation;
            GameTime = gameTime;
            HomeTeamRoster = homeTeamRoster;
            AwayTeamRoster = awayTeamRoster;
        }
    }
}
