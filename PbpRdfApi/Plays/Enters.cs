using System;
using System.Collections.Generic;
using InCube.Core.Functional;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class Enters : Play
    {
        /// <summary>
        /// Gets the IRI of the player that exited.
        /// </summary>
        /// <value>The player iri.</value>
        public readonly Option<string> PlayerExitingIri = Option<string>.None;

        /// <summary>
        /// Gets the IRI of the player that entered.
        /// </summary>
        /// <value>The player iri.</value>
        public readonly Option<string> PlayerEnteringIri = Option<string>.None;

        public Enters(IEnumerable<Triple> triples) : base(triples)
        {
            foreach (Triple triple in _triples)
            {
                switch (triple.Predicate.ToString().Replace("http://stellman-greene.com/pbprdf#", ""))
                {
                    case "playerExiting":
                        PlayerExitingIri = triple.Object.ToString();
                        break;
                    case "playerEntering":
                        PlayerEnteringIri = triple.Object.ToString();
                        break;
                }
            }
        }
    }
}
