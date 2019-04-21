using System;
using System.IO;
using System.Reflection;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Storage;

namespace PbpRdfApiTests
{
    public static class TestData
    {
        public static PersistentTripleStore TripleStore
        {
            get
            {
                var inMemoryManager = new InMemoryManager();
                var persistentTripleStore = new PersistentTripleStore(inMemoryManager);
                persistentTripleStore.Add(Graph("two-wnba-2018-playoff-games.ttl", "http://stellman-greene.com/pbprdf/wnba-2018-playoffs"));
                persistentTripleStore.Add(Graph("ontology.ttl", "http://stellman-greene.com/pbprdf/ontology"));
                persistentTripleStore.Flush();
                return persistentTripleStore;
            }
        }

        private static IGraph Graph(string resourceFilename, string uri)
        {

            IGraph graph = new QueryableGraph();
            graph.BaseUri = new Uri(uri);
            var ttlparser = new TurtleParser();
            var ttl = GetStreamResource(resourceFilename);
            ttlparser.Load(graph, ttl);
            return graph;
        }

        static TextReader GetStreamResource(string filename)
        {
            var asm = Assembly.GetExecutingAssembly();
            var stream = asm.GetManifestResourceStream($"PbpRdfApiTests.TestData.{filename}");
            StreamReader reader = new StreamReader(stream);
            return reader;
        }
    }
}
