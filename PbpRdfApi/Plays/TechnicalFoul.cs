using System;
using System.Collections.Generic;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class TechnicalFoul : Play
    {
        public TechnicalFoul(IEnumerable<Triple> triples) : base(triples)
        {
        }
    }
}
