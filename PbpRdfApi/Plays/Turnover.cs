using System;
using System.Collections.Generic;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class Turnover : Play
    {
        public Turnover(IEnumerable<Triple> triples) : base(triples)
        {
        }
    }
}
