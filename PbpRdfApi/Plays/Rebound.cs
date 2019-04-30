using System;
using System.Collections.Generic;
using InCube.Core.Functional;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class Rebound : Play
    {
        /// <summary>
        /// The player that got this <see cref="T:PbpRdfApi.Plays.Rebound"/> rebound.
        /// </summary>
        /// <value>The player iri.</value>
        public readonly Option<string> ReboundedByPlayerIri = Option<string>.None;

        /// <summary>
        /// Determines whether this <see cref="T:PbpRdfApi.Plays.Rebound"/> rebound is offensive.
        /// </summary>
        /// <value><c>true</c> if rebound was offensive.</value>
        public readonly Option<bool> IsOffensive = Option<bool>.None;


        public Rebound(IEnumerable<Triple> triples) : base(triples)
        {
            foreach (Triple triple in _triples)
            {
                Option<bool> TripleBoolValue()
                {
                    if (triple.Object is LiteralNode)
                    {
                        var node = triple.Object as LiteralNode;
                        if (bool.TryParse(node.Value, out bool b)) return b;
                    }
                    return Option<bool>.None;
                }

                switch (triple.Predicate.ToString().Replace("http://stellman-greene.com/pbprdf#", ""))
                {
                    case "reboundedBy":
                        ReboundedByPlayerIri = triple.Object.ToString();
                        break;
                    case "isOffensive":
                        IsOffensive = TripleBoolValue();
                        break;
                }
            }
        }
    }
}
