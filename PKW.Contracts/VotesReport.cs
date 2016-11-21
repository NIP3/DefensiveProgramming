using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PKW.Contracts
{
    /// <summary>
    /// Protokół wyborczy – raport oddanych głosów
    /// </summary>
    [DataContract]
    public class VotesReport
    {
        /// <summary>
        /// Id okręgu wyborczego
        /// </summary>
        [DataMember]
        public int ConstituencyId { get; set; }

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
        /// Liczby głosów oddanych na poszczególnych kandydatów.
        /// </summary>
        [DataMember]
        public IEnumerable<CandidateVotes> Votes { get; set; }
    }
}