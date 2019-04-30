using System;
using System.Collections.Generic;
using InCube.Core.Functional;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class Shot : Play
    {
        /// <summary>
        /// Determines whether this <see cref="T:PbpRdfApi.Plays.Shot"/> shot was made.
        /// </summary>
        /// <value><c>true</c> if shot was made; otherwise, <c>false</c>.</value>
        public readonly Option<bool> ShotMade = Option<bool>.None;

        /// <summary>
        /// The IRI of the player that took this <see cref="T:PbpRdfApi.Plays.Shot"/> shot.
        /// </summary>
        /// <value>The player iri.</value>
        public readonly Option<string> PlayerIri = Option<string>.None;

        /// <summary>
        /// The type of this <see cref="T:PbpRdfApi.Plays.Shot"/> shot.
        /// </summary>
        /// <value>The type of the shot.</value>
        public readonly Option<string> ShotType = Option<string>.None;

        /// <summary>
        /// The IRI of the player that assisted this <see cref="T:PbpRdfApi.Plays.Shot"/> shot.
        /// </summary>
        /// <value>The player iri, or None if no player assisted.</value>
        public readonly Option<string> AssistedByPlayerIri = Option<string>.None;

        /// <summary>
        /// The number of points scored by this <see cref="T:PbpRdfApi.Plays.Shot"/> shot.
        /// </summary>
        /// <value>The number of points scored (0 if not made).</value>
        public readonly Option<int> Points = Option<int>.None;

        public Shot(IEnumerable<Triple> triples) : base(triples)
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
                    case "shotMade":
                        ShotMade = TripleBoolValue();
                        break;
                    case "shotBy":
                        PlayerIri = triple.Object.ToString();
                        break;
                    case "shotType":
                        ShotType = triple.Object.ToString();
                        break;
                    case "shotAssistedBy":
                        AssistedByPlayerIri = triple.Object.ToString();
                        break;
                    case "shotPoints":
                        Points = TripleIntValue();
                        break;
                }
            }
        }
    }
}
