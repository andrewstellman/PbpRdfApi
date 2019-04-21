using System;
using System.Collections.Generic;
using VDS.RDF;
using System.Linq;

namespace PbpRdfApi
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
        public string Iri { get; private set; }

        /// <summary>
        /// IRI of the game this play occurred in.
        /// </summary>
        /// <value>The game iri.</value>
        public string GameIri { get; private set; }

        /// <summary>
        /// IRI fo the team this play is for.
        /// </summary>
        /// <value>The team iri.</value>
        public string TeamIri { get; private set; }

        /// <summary>
        /// The game time of this play (e.g. "8:41").
        /// </summary>
        /// <value>The time.</value>
        public string Time { get; private set; }

        /// <summary>
        /// The period this play occrured in, including overtime (e.g.
        /// for an NCAAW game overtime starts with period 5, for
        /// an NCAAM game overtame starts with period 3).
        /// </summary>
        /// <value>The period.</value>
        public int Period { get; private set; }

        /// <summary>
        /// The number of seconds into the game this play takes occurred.
        /// </summary>
        /// <value>The seconds into game.</value>
        public int SecondsIntoGame { get; private set; }

        /// <summary>
        /// The number of seconds before the end of the period this play takes place.
        /// </summary>
        /// <value>The seconds left in period.</value>
        public int SecondsLeftInPeriod { get; private set; }

        /// <summary>
        /// Gets the sequential event number for this play.
        /// </summary>
        /// <value>The event number.</value>
        public int EventNumber { get; private set; }

        /// <summary>
        /// Gets the IRI of the previous event in the game.
        /// </summary>
        /// <value>The previous event iri.</value>
        public string PreviousEventIri { get; private set; }

        /// <summary>
        /// Gets the IRI of the next event in the game.
        /// </summary>
        /// <value>The next event iri.</value>
        public string NextEventIri { get; private set; }

        /// <summary>
        /// Gets the number of seconds since the previous event.
        /// </summary>
        /// <value>The seconds since previous event.</value>
        public int SecondsSincePreviousEvent { get; private set; }

        /// <summary>
        /// Gets the seconds until the next event.
        /// </summary>
        /// <value>The number of seconds until next event.</value>
        public int SecondsUntilNextEvent { get; private set; }

        /// <summary>
        /// Gets the score for the home team.
        /// </summary>
        /// <value>The score for the home team.</value>
        public int HomeScore { get; private set; }

        /// <summary>
        /// Gets the score for the away team.
        /// </summary>
        /// <value>The score for the away team.</value>
        public int AwayScore { get; private set; }

        /// <summary>
        /// The label of the play.
        /// </summary>
        /// <value>The label.</value>
        public string Label { get; private set; }

        /// <summary>
        /// The RDF triples for this play.
        /// </summary>
        protected List<Triple> _triples;

        public Play(IEnumerable<Triple> triples)
        {
            _triples = new List<Triple>(triples);

            Iri = _triples.First().Subject.ToString();

            foreach (Triple triple in _triples)
            {
                int TripleIntValue()
                {
                    int i = -1;
                    if (triple.Object is LiteralNode)
                    {
                        var node = triple.Object as LiteralNode;
                        if (int.TryParse(node.Value, out i)) return i;
                    }
                    return i;
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