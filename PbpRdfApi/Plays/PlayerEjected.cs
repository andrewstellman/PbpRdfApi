using System;
using System.Collections.Generic;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class PlayerEjected : Play
    {
        public PlayerEjected(IEnumerable<Triple> triples) : base(triples)
        {
        }
    }
}
