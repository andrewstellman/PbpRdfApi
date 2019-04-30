using System;
using System.Collections.Generic;
using InCube.Core.Functional;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class Block : Shot
    {

        /// <summary>
        /// Gets the IRI of the player that blocked the shot.
        /// </summary>
        /// <value>The player iri.</value>
        public readonly Option<string> BlockedByPlayerIri = Option<string>.None;

        public Block(IEnumerable<Triple> triples) : base(triples)
        {
            foreach (Triple triple in _triples)
            {
                switch (triple.Predicate.ToString().Replace("http://stellman-greene.com/pbprdf#", ""))
                {
                    case "shotBlockedBy":
                        BlockedByPlayerIri = triple.Object.ToString();
                        break;
                }
            }
        }
    }
}
