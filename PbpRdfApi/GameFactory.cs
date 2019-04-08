using System;
using System.Collections.Generic;
using System.Linq;
using InCube.Core.Functional;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;

namespace PbpRdfApi
{

    /// <summary>
    /// Factory to populate Game objects from an RDF TripleStore
    /// </summary>
    public class GameFactory
    {
        private readonly PersistentTripleStore _tripleStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PbpRdfApi.GameFactory"/> class.
        /// </summary>
        /// <param name="persistentTripleStore">Persistent triple store to read game data from.</param>
        public GameFactory(PersistentTripleStore persistentTripleStore)
        {
            _tripleStore = persistentTripleStore;
        }

        /// <summary>
        /// Fetch a game from the TripleStore by its <paramref name="gameIri"/>.
        /// </summary>
        /// <returns>The game.</returns>
        /// <param name="gameIri">IRI of the game to fetch.</param>
        public Game Game(string gameIri)
        {
            var query = $@"BASE <http://stellman-greene.com/> 
PREFIX pbprdf: <pbprdf#>
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
SELECT * {{
  ?game a pbprdf:Game .
  ?game rdfs:label ?label .
  OPTIONAL {{ ?game pbprdf:gameLocation ?gameLocation . }}
  ?game pbprdf:gameTime ?gameTime .
  ?game pbprdf:homeTeam ?homeTeam .
  ?game pbprdf:awayTeam ?awayTeam .
}} VALUES (?game) {{
 (<{gameIri}>) 
}}";

            SparqlResultSet results = _tripleStore.ExecuteQuery(query) as SparqlResultSet;
            if (results.Count < 1) throw new NotFoundException($"Game <{gameIri}> not found");
            var result = results.First();

            string getString(string variable) => !result.HasValue(variable)
                ? throw new InvalidDataException($"Game <{gameIri}> has no pbprdf:{variable} value")
                : result[variable].ToString();

            var homeTeamIri = getString("homeTeam");
            var awayTeamIri = getString("awayTeam");

            var gameLocation = result.HasValue("gameLocation") ? getString("gameLocation") : Option<string>.None;

            var gameTimeLiteral = result["gameTime"] as ILiteralNode;
            Option<DateTimeOffset> gameTime = (gameTimeLiteral is null) ? Option<DateTimeOffset>.None
                : (gameTimeLiteral.DataType.ToString() != XmlSpecsHelper.XmlSchemaDataTypeDateTime) ? Option<DateTimeOffset>.None
                : !DateTimeOffset.TryParse(gameTimeLiteral.Value, out DateTimeOffset dateTimeOffset) ? Option<DateTimeOffset>.None
                : dateTimeOffset;

            /// <summary>
            /// Gets the home or away roster from the TripleStore.
            /// </summary>
            /// <returns>The roster.</returns>
            /// <param name="rosterType">Home or away.</param>
            IDictionary<string, string> GetRoster(string rosterType) =>
                (_tripleStore.ExecuteQuery($@"PREFIX pbprdf: <http://stellman-greene.com/pbprdf#>
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
SELECT ?player ?label {{
  ?game a pbprdf:Game .
  ?game pbprdf:has{rosterType}TeamRoster / pbprdf:hasPlayer ?player .
  ?player rdfs:label ?label .
}} VALUES (?game) {{
 (<{gameIri}>) 
}}")
                as SparqlResultSet)
                .ToDictionary(rosterResult => rosterResult["player"].ToString(),
                rosterResult => rosterResult["label"].ToString());

            var homeTeamRoster = GetRoster("Home");
            var awayTeamRoster = GetRoster("Away");

            var game = new Game(homeTeamIri, awayTeamIri, gameLocation, gameTime, homeTeamRoster, awayTeamRoster);

            return game;
        }
    }

}
