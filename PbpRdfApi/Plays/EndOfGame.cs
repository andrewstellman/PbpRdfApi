using System;
using System.Collections.Generic;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class EndOfGame : Play
    {
        public EndOfGame(IEnumerable<Triple> triples) : base(triples) { }
    }
}
