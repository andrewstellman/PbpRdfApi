using System;
using System.Collections.Generic;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class Enters : Play
    {
        public Enters(IEnumerable<Triple> triples) : base(triples)
        {
        }
    }
}
