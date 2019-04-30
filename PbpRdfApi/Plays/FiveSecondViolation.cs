using System;
using System.Collections.Generic;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class FiveSecondViolation : Play
    {
        public FiveSecondViolation(IEnumerable<Triple> triples) : base(triples) { }        
    }
}
