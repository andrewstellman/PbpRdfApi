using System;
using System.Collections.Generic;
using InCube.Core.Functional;
using VDS.RDF;

namespace PbpRdfApi.Plays
{
    public class Timeout : Play
    {
        /// <summary>
        /// The duration of this <see cref="T:PbpRdfApi.Plays.Timeout"/> timeout.
        /// </summary>
        /// <value>The duration of the timeout (e.g. "Full").</value>
        public readonly Option<string> TimeoutDuration = Option<string>.None;

        /// <summary>
        /// Determines whether this <see cref="T:PbpRdfApi.Plays.Timeout"/> timeout is official.
        /// </summary>
        /// <value><c>true</c> if shot was made.</value>
        public readonly Option<bool> IsOfficial = Option<bool>.None;

        public Timeout(IEnumerable<Triple> triples) : base(triples)
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
                    case "timeoutDuration":
                        TimeoutDuration = triple.Object.ToString();
                        break;
                    case "isOfficial":
                        IsOfficial = TripleBoolValue();
                        break;
                }
            }
        }
    }
}
