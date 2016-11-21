using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace PKW.Contracts
{
    /// <summary>
    /// Liczba głosów oddanych na kandydata.
    /// </summary>
    [DataContract]
    public class CandidateVotes : IEquatable<CandidateVotes>
    {
        /// <summary>
        /// Identyfikator kandydata
        /// </summary>
        [DataMember]
        public int CandidateId { get; set; }

        /// <summary>
        /// Nazwa kandydata
        /// </summary>
        [DataMember]
        public string CandidateName { get; set; }

        /// <summary>
        /// Liczba oddanych głosów
        /// </summary>
        [DataMember]
        public int Amount { get; set; }

        /// <summary>
        /// Procentowo liczba otrzymanych głosów
        /// </summary>
        [DataMember]
        public double Percentage { get; set; }


        public override int GetHashCode()
        {
            return CandidateId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var otherCandidateVotes = obj as CandidateVotes;
            return otherCandidateVotes != null && Equals(otherCandidateVotes);
        }

        public bool Equals(CandidateVotes other)
        {
            return this.CandidateName == other.CandidateName
                   && this.CandidateId == other.CandidateId
                   && this.Amount == other.Amount;
        }
    }
}