using System;
using System.Collections.Generic;
using InCube.Core.Functional;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class Turnover : Play
    {
        /// <summary>
        /// The type of the turnover.
        /// </summary>
        /// <value>The player iri, or None if no player stole the ball.</value>
        public readonly Option<string> TurnoverType = Option<string>.None;

        /// <summary>
        /// The IRI of the player that stole the ball.
        /// </summary>
        /// <value>The player iri, or None if no player stole the ball.</value>
        public readonly Option<string> StolenByPlayerIri = Option<string>.None;

        /// <summary>
        /// The IRI of the player that turned over the ball.
        /// </summary>
        /// <value>The player iri, or None if no player was responsible for the turnover.</value>
        public readonly Option<string> TurnedOverByPlayerIri = Option<string>.None;

        public Turnover(IEnumerable<Triple> triples) : base(triples)
        {
            foreach (Triple triple in _triples)
            {
                switch (triple.Predicate.ToString().Replace("http://stellman-greene.com/pbprdf#", ""))
                {
                    case "turnoverType":
                        TurnoverType = triple.Object.ToString();
                        break;
                    case "stolenBy":
                        StolenByPlayerIri = triple.Object.ToString();
                        break;
                    case "turnedOverBy":
                        TurnedOverByPlayerIri = triple.Object.ToString();
                        break;
                }
            }
        }
    }
}
