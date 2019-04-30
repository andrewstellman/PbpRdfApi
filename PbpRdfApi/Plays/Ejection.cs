using System;
using System.Collections.Generic;
using InCube.Core.Functional;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class Ejection : Play
    {
        /// <summary>
        /// Gets the IRI of the player that was ejected.
        /// </summary>
        /// <value>The player iri.</value>
        public readonly Option<string> PlayerEjectedIri = Option<string>.None;

        public Ejection(IEnumerable<Triple> triples) : base(triples)
        {
            foreach (Triple triple in _triples)
            {
                switch (triple.Predicate.ToString().Replace("http://stellman-greene.com/pbprdf#", ""))
                {
                    case "playerEjected":
                        PlayerEjectedIri = triple.Object.ToString();
                        break;
                }
            }
        }
    }
}
