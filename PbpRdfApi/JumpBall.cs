using System;
using System.Collections.Generic;
using System.Linq;
using VDS.RDF;

namespace PbpRdfApi
{
    public class JumpBall : Play
    {
        /// <summary>
        /// Gets the IRI of the home player contesting the jump ball.
        /// </summary>
        /// <value>The home player iri.</value>
        public string HomePlayerIri { get; private set; }

        /// <summary>
        /// Gets the IRI of the away player contesting the jump ball.
        /// </summary>
        /// <value>The home player iri.</value>
        public string AwayPlayerIri { get; private set; }


        /// <summary>
        /// Gets the IRI of the player who gained possession of the jump ball.
        /// </summary>
        /// <value>The home player iri.</value>
        public string JumpBallGainedPossessionIri { get; private set; }

        public JumpBall(IEnumerable<Triple> triples) : base(triples)
        {
            foreach (Triple triple in _triples)
            {
                switch (triple.Predicate.ToString().Replace("http://stellman-greene.com/pbprdf#", ""))
                {
                    case "jumpBallHomePlayer":
                        HomePlayerIri = triple.Object.ToString();
                        break;
                    case "jumpBallAwayPlayer":
                        AwayPlayerIri = triple.Object.ToString();
                        break;
                    case "jumpBallGainedPossession":
                        JumpBallGainedPossessionIri = triple.Object.ToString();
                        break;
                }
            }
        }
    }
}
