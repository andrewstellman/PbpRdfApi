using System;
using System.Linq;
using VDS.RDF;
using VDS.RDF.Query;

namespace PbpRdfApi
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
    GRAPH ?ontologyGraph {{
        ?type rdfs:subClassOf pbprdf:Play .
    }}
}}";

            SparqlResultSet results = _tripleStore.ExecuteQuery(query) as SparqlResultSet;
            if (results.Count < 1) throw new NotFoundException($"Unable to find play #{eventNumber} in game <{gameIri}>");
            var result = results.First();

            var graphUri = result["graph"];
            var graph = _tripleStore.Graphs
                .First(g => g.BaseUri.ToString() == graphUri.ToString());

            var play = result["play"];
            var triples = graph.GetTriplesWithSubject(play);

            var playType = result["type"].ToString();
            switch (playType)
            {
                case "http://stellman-greene.com/pbprdf#JumpBall":
                    return new JumpBall(triples);
                default:
                    throw new InvalidDataException($"Invalid type <{playType}> for play #{eventNumber} in game <{gameIri}> ");
            }
        }
    }
}
