using System;
using System.Collections.Generic;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class Foul : Play
    {
        public Foul(IEnumerable<Triple> triples) : base(triples)
        {
        }
    }
}
