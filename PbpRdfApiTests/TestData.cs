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
                persistentTripleStore.Add(Graph);
                persistentTripleStore.Flush();
                return persistentTripleStore;
            }
        }

        public static IGraph Graph
        {
            get
            {
                var ttl = GetStreamResource("two-wnba-2018-playoff-games.ttl");
                IGraph graph = new QueryableGraph();
                var ttlparser = new TurtleParser();
                ttlparser.Load(graph, ttl);
                return graph;
            }
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
