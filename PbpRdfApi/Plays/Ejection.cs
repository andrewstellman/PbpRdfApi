using System;
using System.Collections.Generic;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class Ejection : Play
    {
        public Ejection(IEnumerable<Triple> triples) : base(triples)
        {
        }
    }
}
