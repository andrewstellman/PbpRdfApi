using System;
using System.Collections.Generic;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class Timeout : Play
    {
        public Timeout(IEnumerable<Triple> triples) : base(triples)
        {
        }
    }
}
