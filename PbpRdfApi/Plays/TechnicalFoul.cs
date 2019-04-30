using System;
using System.Collections.Generic;
using InCube.Core.Functional;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class TechnicalFoul : Play
    {

        /// <summary>
        /// Determines if this <see cref="T:PbpRdfApi.Plays.TechnicalFoul"/> technical foul is a three second violation.
        /// </summary>
        /// <value><c>true</c> if it's a three second violation; otherwise, <c>false</c>.</value>
        public readonly bool IsThreeSecond = false;

        /// <summary>
        /// Determines if this <see cref="T:PbpRdfApi.Plays.TechnicalFoul"/> technical foul is a delay of game violation.
        /// </summary>
        /// <value><c>true</c> if it's a delay of game violation; otherwise, <c>false</c>.</value>
        public readonly bool IsDelayOfGame = false;

        /// <summary>
        /// The technical foul number for the player.
        /// </summary>
        public readonly Option<int> TechnicalFoulNumber = Option<int>.None;

        /// <summary>
        /// Gets the IRI of the player that committed the foul.
        /// </summary>
        /// <value>The player iri, or None if no player committed the foul.</value>
        public readonly Option<string> FoulCommittedByPlayerIri = Option<string>.None;

        public TechnicalFoul(IEnumerable<Triple> triples) : base(triples)
        {
            foreach (Triple triple in _triples)
            {
                Option<int> TripleIntValue()
                {
                    if (triple.Object is LiteralNode)
                    {
                        var node = triple.Object as LiteralNode;
                        if (int.TryParse(node.Value, out int i)) return i;
                    }
                    return Option<int>.None;
                }

                bool TripleBoolValue(bool defaultValue) 
                {
                    if (triple.Object is LiteralNode)
                    {
                        var node = triple.Object as LiteralNode;
                        if (bool.TryParse(node.Value, out bool b)) return b;
                    }
                    return defaultValue;
                }

                switch (triple.Predicate.ToString().Replace("http://stellman-greene.com/pbprdf#", ""))
                {
                    case "isThreeSecond":
                        IsThreeSecond = TripleBoolValue(false);
                        break;
                    case "isDelayOfGame":
                        IsDelayOfGame = TripleBoolValue(false);
                        break;
                    case "technicalFoulNumber":
                        TechnicalFoulNumber = TripleIntValue();
                        break;
                    case "foulCommittedBy":
                        FoulCommittedByPlayerIri = triple.Object.ToString();
                        break;
                }
            }
        }
    }
}
