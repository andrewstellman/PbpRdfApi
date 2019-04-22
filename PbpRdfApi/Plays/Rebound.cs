using System;
using System.Collections.Generic;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class Rebound : Play
    {
        public Rebound(IEnumerable<Triple> triples) : base(triples)
        {
        }
    }
}
