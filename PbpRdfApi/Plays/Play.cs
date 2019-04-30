using System;
using System.Collections.Generic;
using VDS.RDF;
using System.Linq;
using InCube.Core.Functional;

namespace PbpRdfApi.Plays
{
    /// <summary>
    /// Superclass for all plays.
    /// </summary>
    public abstract class Play : IEquatable<Play>
    {
        /// <summary>
        /// IRI of this play.
        /// </summary>
        /// <value>The play iri.</value>
        public readonly string Iri;

        /// <summary>
        /// IRI of the game this play occurred in.
        /// </summary>
        /// <value>The game iri.</value>
        public readonly string GameIri;

        /// <summary>
        /// IRI fo the team this play is for.
        /// </summary>
        /// <value>The team iri.</value>
        public readonly string TeamIri;

        /// <summary>
        /// The game time of this play (e.g. "8:41").
        /// </summary>
        /// <value>The time.</value>
        public readonly string Time;

        /// <summary>
        /// The period this play occrured in, including overtime (e.g.
        /// for an NCAAW game overtime starts with period 5, for
        /// an NCAAM game overtame starts with period 3).
        /// </summary>
        /// <value>The period.</value>
        public readonly Option<int> Period = Option<int>.None;

        /// <summary>
        /// The number of seconds into the game this play takes occurred.
        /// </summary>
        /// <value>The seconds into game.</value>
        public readonly Option<int> SecondsIntoGame = Option<int>.None;

        /// <summary>
        /// The number of seconds before the end of the period this play takes place.
        /// </summary>
        /// <value>The seconds left in period.</value>
        public readonly Option<int> SecondsLeftInPeriod = Option<int>.None;

        /// <summary>
        /// Gets the sequential event number for this play.
        /// </summary>
        /// <value>The event number.</value>
        public readonly Option<int> EventNumber = Option<int>.None;

        /// <summary>
        /// Gets the IRI of the previous event in the game.
        /// </summary>
        /// <value>The previous event iri.</value>
        public readonly Option<string> PreviousEventIri = Option<string>.None;

        /// <summary>
        /// Gets the IRI of the next event in the game.
        /// </summary>
        /// <value>The next event iri.</value>
        public readonly Option<string> NextEventIri = Option<string>.None;

        /// <summary>
        /// Gets the number of seconds since the previous event.
        /// </summary>
        /// <value>The seconds since previous event.</value>
        public readonly Option<int> SecondsSincePreviousEvent = Option<int>.None;

        /// <summary>
        /// Gets the seconds until the next event.
        /// </summary>
        /// <value>The number of seconds until next event.</value>
        public readonly Option<int> SecondsUntilNextEvent = Option<int>.None;

        /// <summary>
        /// Gets the score for the home team.
        /// </summary>
        /// <value>The score for the home team.</value>
        public readonly Option<int> HomeScore = Option<int>.None;

        /// <summary>
        /// Gets the score for the away team.
        /// </summary>
        /// <value>The score for the away team.</value>
        public readonly Option<int> AwayScore = Option<int>.None;

        /// <summary>
        /// The label of the play.
        /// </summary>
        /// <value>The label.</value>
        public readonly Option<string> Label = Option<string>.None;

        /// <summary>
        /// The RDF triples for this play.
        /// </summary>
        protected List<Triple> _triples;

        public Play(IEnumerable<Triple> triples)
        {
            _triples = triples.ToList<Triple>();

            Iri = _triples.First().Subject.ToString();

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

                switch (triple.Predicate.ToString().Replace("http://stellman-greene.com/pbprdf#", ""))
                {
                    case "inGame":
                        GameIri = triple.Object.ToString();
                        break;
                    case "forTeam":
                        TeamIri = triple.Object.ToString();
                        break;
                    case "time":
                        Time = triple.Object.ToString();
                        break;
                    case "period":
                        Period = TripleIntValue();
                        break;
                    case "secondsIntoGame":
                        SecondsIntoGame = TripleIntValue();
                        break;
                    case "secondsLeftInPeriod":
                        SecondsLeftInPeriod = TripleIntValue();
                        break;
                    case "eventNumber":
                        EventNumber = TripleIntValue();
                        break;
                    case "previousEvent":
                        PreviousEventIri = triple.Object.ToString();
                        break;
                    case "nextEvent":
                        NextEventIri = triple.Object.ToString();
                        break;
                    case "secondsSincePreviousEvent":
                        SecondsSincePreviousEvent = TripleIntValue();
                        break;
                    case "secondsUntilNextEvent":
                        SecondsUntilNextEvent = TripleIntValue();
                        break;
                    case "homeScore":
                        HomeScore = TripleIntValue();
                        break;
                    case "awayScore":
                        AwayScore = TripleIntValue();
                        break;
                    case "http://www.w3.org/2000/01/rdf-schema#label":
                        Label = triple.Object.ToString();
                        break;
                }
            }

        }

        #region IEquatable<Event> members

        public override bool Equals(object obj)
        {
            return Equals(obj as Play);
        }

        public bool Equals(Play other)
        {
            return other != null &&
                   Iri == other.Iri &&
                   GameIri == other.GameIri &&
                   TeamIri == other.TeamIri &&
                   Time == other.Time &&
                   Period == other.Period &&
                   EventNumber == other.EventNumber;
        }

        public override int GetHashCode()
        {
            var hashCode = -1327815626;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Iri);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GameIri);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TeamIri);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Time);
            hashCode = hashCode * -1521134295 + Period.GetHashCode();
            hashCode = hashCode * -1521134295 + EventNumber.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Play left, Play right)
        {
            return EqualityComparer<Play>.Default.Equals(left, right);
        }

        public static bool operator !=(Play left, Play right)
        {
            return !(left == right);
        }

        #endregion
    }
}