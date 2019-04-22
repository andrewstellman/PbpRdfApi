using System;
using System.Collections.Generic;
using InCube.Core.Functional;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class Shot : Play
    {
        /// <summary>
        /// Gets the value of whether this <see cref="T:PbpRdfApi.Plays.Shot"/> shot was made.
        /// </summary>
        /// <value><c>true</c> if shot was made; otherwise, <c>false</c>.</value>
        public bool ShotMade { get; private set; }

        /// <summary>
        /// Gets the IRI of the player that took the shot.
        /// </summary>
        /// <value>The player iri.</value>
        public string PlayerIri { get; private set; }

        /// <summary>
        /// Gets the type of the shot.
        /// </summary>
        /// <value>The type of the shot.</value>
        public string ShotType { get; private set; }

        /// <summary>
        /// Gets the IRI of the player that assisted the shot.
        /// </summary>
        /// <value>The player iri, or None if no player assisted.</value>
        public Option<string> AssistedByPlayerIri { get; private set; }

        /// <summary>
        /// Gets the number of points scored by the shot.
        /// </summary>
        /// <value>The number of points scored (0 if not made).</value>
        public int Points { get; private set; }

        public Shot(IEnumerable<Triple> triples) : base(triples)
        {
            foreach (Triple triple in _triples)
            {
                int TripleIntValue(int defaultValue)
                {
                    int i = defaultValue;
                    if (triple.Object is LiteralNode)
                    {
                        var node = triple.Object as LiteralNode;
                        int.TryParse(node.Value, out i);
                    }
                    return i;
                }

                bool TripleBoolValue(bool defaultValue)
                {
                    bool b = defaultValue;
                    if (triple.Object is LiteralNode)
                    {
                        var node = triple.Object as LiteralNode;
                        bool.TryParse(node.Value, out b);
                    }
                    return b;
                }

                AssistedByPlayerIri = Option<string>.None;

                switch (triple.Predicate.ToString().Replace("http://stellman-greene.com/pbprdf#", ""))
                {
                    case "shotMade":
                        ShotMade = TripleBoolValue(false);
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
                        Points = TripleIntValue(0);
                        break;
                }
            }
        }
    }
}
