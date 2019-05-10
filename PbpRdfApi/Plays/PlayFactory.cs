using System;
using System.Collections.Generic;
using System.Linq;
using VDS.RDF;
using VDS.RDF.Query;

namespace PbpRdfApi.Plays
{
    /// <summary>
    /// Factory to populate Event objects from an RDF TripleStore
    /// </summary>
    public class PlayFactory
    {
        private readonly PersistentTripleStore _tripleStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:PbpRdfApi.GameEventFactory"/> class.
        /// </summary>
        /// <param name="persistentTripleStore">Persistent triple store to read game data from.</param>
        public PlayFactory(PersistentTripleStore persistentTripleStore)
        {
            _tripleStore = persistentTripleStore;
        }

        /// <summary>
        /// Fetch a game from the TripleStore by its <paramref name="gameIri"/> and <paramref name="eventNumber"/>.
        /// </summary>
        /// <returns>The play.</returns>
        /// <param name="gameIri">IRI of the game to fetch the evnet from.</param>
        /// <param name="eventNumber">Event number of the event to fetch.</param>
        public Play Play(string gameIri, int eventNumber)
        {
            var query = $@"BASE <http://stellman-greene.com/> 
PREFIX pbprdf: <pbprdf#>
PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>
PREFIX xsd: <http://www.w3.org/2001/XMLSchema#>
SELECT ?play ?type ?graph {{ 
    GRAPH ?graph {{ 
        ?play pbprdf:inGame <{gameIri}> .
        ?play pbprdf:eventNumber ""{eventNumber}""^^xsd:int .
        ?play a ?type .
    }}
}}";
            SparqlResultSet results = _tripleStore.ExecuteQuery(query) as SparqlResultSet;
            if (results.Count < 1) throw new NotFoundException($"Unable to find event #{eventNumber} in game <{gameIri}>");

            var graphUri = results.First()["graph"];
            var graph = _tripleStore.Graphs
                .First(g => g.BaseUri.ToString() == graphUri.ToString());

            var playIris = results.Select(r => r["play"].ToString()).Distinct();
            if (playIris.Count() != 1) throw new InvalidDataException($"{playIris.Count()} plays found with event #{eventNumber} in game <{gameIri}>");
            var playNode = results.First()["play"];

            var triples = graph.GetTriplesWithSubject(playNode).ToList<Triple>();

            var playTypes = results.Select(r => r["type"].ToString().StripPbpRdfPrefix()).ToList<string>();

            return ReturnPlayType(playTypes, triples, eventNumber, gameIri);
        }

        /// <summary>
        /// Casts the play as a subclass of Play.
        /// </summary>
        /// <returns>A subclass of Play populated with data from the play triples.</returns>
        /// <param name="playTypes">The play types extracted from rdf:type triples and stripled of the pbprdf: prefixes.</param>
        /// <param name="triples">The triples for the play.</param>
        /// <param name="eventNumber">The event number (only for an exception message).</param>
        /// <param name="gameIri">The game IRI (only for an exception message).</param>
        private Play ReturnPlayType(List<string> playTypes, IEnumerable<Triple> triples, int eventNumber, string gameIri)
        {
            if (playTypes.Contains("Ejection")) return new Ejection(triples);
            if (playTypes.Contains("EndOfGame")) return new EndOfGame(triples);
            if (playTypes.Contains("EndOfPeriod")) return new EndOfPeriod(triples);
            if (playTypes.Contains("Enters")) return new Enters(triples);
            if (playTypes.Contains("FiveSecondViolation")) return new FiveSecondViolation(triples);
            if (playTypes.Contains("Foul")) return new Foul(triples);
            if (playTypes.Contains("JumpBall")) return new JumpBall(triples);
            if (playTypes.Contains("Rebound")) return new Rebound(triples);
            if (playTypes.Contains("TechnicalFoul")) return new TechnicalFoul(triples);
            if (playTypes.Contains("Timeout")) return new Timeout(triples);
            if (playTypes.Contains("Turnover")) return new Turnover(triples);

            /* 
             * pbprdf:Block is an rdf:subClassOf pbprdf:Shot
             * a pbbrdf:Block play is rdf:type pbprdf:Shot, so we need to test for block first
             */
            if (playTypes.Contains("Block")) return new Block(triples);
            if (playTypes.Contains("Shot")) return new Shot(triples);

            var s = playTypes.Count == 1 ? "" : "s";
            throw new InvalidDataException($"Invalid type{s} {string.Join(", ", playTypes)} for play #{eventNumber} in game <{gameIri}> ");
        }
    }
}
