using System;
using System.Collections.Generic;
using InCube.Core.Functional;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class Foul : Play
    {
        /// <summary>
        /// The IRI of the player that committed this <see cref="T:PbpRdfApi.Plays.Foul"/> foul.
        /// </summary>
        /// <value>The player iri.</value>
        public readonly Option<string> CommittedByPlayerIri = Option<string>.None;

        /// <summary>
        /// The IRI of the player that drew this <see cref="T:PbpRdfApi.Plays.Foul"/> foul.
        /// </summary>
        /// <value>The player iri.</value>
        public readonly Option<string> DrawnByPlayerIri = Option<string>.None;

        /// <summary>
        /// Determines whether this <see cref="T:PbpRdfApi.Plays.Foul"/> foul is offensive.
        /// </summary>
        /// <value><c>true</c> if this is an offensive foul; otherwise, <c>false</c>.</value>
        public readonly Option<bool> IsOffensive = Option<bool>.None;

        /// <summary>
        /// Determines whether this <see cref="T:PbpRdfApi.Plays.Foul"/> foul is a shooting foul.
        /// </summary>
        /// <value><c>true</c> if this is a shooting foul; otherwise, <c>false</c>.</value>
        public readonly Option<bool> IsShootingFoul = Option<bool>.None;

        /// <summary>
        /// Determines whether this <see cref="T:PbpRdfApi.Plays.Foul"/> foul is a loose ball foul.
        /// </summary>
        /// <value><c>true</c> if this is a loose ball foul; otherwise, <c>false</c>.</value>
        public readonly Option<bool> IsLooseBallFoul = Option<bool>.None;


        /// <summary>
        /// Determines whether this <see cref="T:PbpRdfApi.Plays.Foul"/> foul is a charge.
        /// </summary>
        /// <value><c>true</c> if this is a charge foul; otherwise, <c>false</c>.</value>
        public readonly Option<bool> IsCharge = Option<bool>.None;

        /// <summary>
        /// Determines whether this <see cref="T:PbpRdfApi.Plays.Foul"/> foul is a personal blocking foul.
        /// </summary>
        /// <value><c>true</c> if this is a personal blocking foul; otherwise, <c>false</c>.</value>
        public readonly Option<bool> IsPersonalBlockingFoul = Option<bool>.None;

        public Foul(IEnumerable<Triple> triples) : base(triples)
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
                    case "foulCommittedBy":
                        CommittedByPlayerIri = triple.Object.ToString();
                        break;
                    case "foulDrawnBy":
                        DrawnByPlayerIri = triple.Object.ToString();
                        break;
                    case "isOffensive":
                        IsOffensive = TripleBoolValue();
                        break;
                    case "isShootingFoul":
                        IsOffensive = TripleBoolValue();
                        break;
                    case "isLooseBallFoul":
                        IsLooseBallFoul = TripleBoolValue();
                        break;
                    case "isCharge":
                        IsCharge = TripleBoolValue();
                        break;
                    case "isPersonalBlockingFoul":
                        IsPersonalBlockingFoul = TripleBoolValue();
                        break;
                }
            }
        }
    }
}
