using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PKW.Contracts
{
    /// <summary>
    /// Wyniki Głosowania
    /// </summary>
    [DataContract]
    public class VotingSummary : IEquatable<VotingSummary>
    {
        /// <summary>
        /// Liczba głosów nieważnych.
        /// </summary>
        [DataMember]
        public int InvalidVotes { get; set; }

        /// <summary>
        /// Liczba wydanych kart do głosowania.
        /// </summary>
        [DataMember]
        public int IssuedBallots { get; set; }

        /// <summary>
        /// Podliczone głosy na poszczególnych kandydatów.
        /// </summary>
        [DataMember]
        public IEnumerable<CandidateVotes> AggregatedVoteses { get; set; }


        public override int GetHashCode()
        {
            return string.Join("|", InvalidVotes, IssuedBallots, AggregatedVoteses.GetHashCode()).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            VotingSummary other = obj as VotingSummary;
            return other != null && Equals(other);
        }

        public bool Equals(VotingSummary other)
        {
            return other != null
                   && this.InvalidVotes == other.InvalidVotes
                   && this.IssuedBallots == other.IssuedBallots
                   && this.AggregatedVoteses.SequenceEqual(other.AggregatedVoteses);
        }
    }
}