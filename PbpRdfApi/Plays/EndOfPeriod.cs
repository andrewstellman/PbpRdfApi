using System;
using System.Collections.Generic;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class EndOfPeriod : Play
    {
        public EndOfPeriod(IEnumerable<Triple> triples) : base(triples) { }
    }
}
