using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Text;

namespace PKW.Contracts
{
    /// <summary>
    /// Kandydat sartujący w wyborach
    /// </summary>
    [DataContract]
    public class Candidate : IEquatable<Candidate>
    {
        /// <summary>
        /// Id Kandydata.
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Nazwa Kandydata
        /// </summary>
        [DataMember]
        public string DisplayName { get; set; }

        /// <summary>
        /// Numer okgręgu wyborczego, w którym startuje w wyborach.
        /// </summary>
        [DataMember]
        public int ConstituencyId { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Candidate other = obj as Candidate;
            return other != null && Equals(other);
        }

        public bool Equals(Candidate other)
        {
            return this.Id == other.Id
                   && this.DisplayName == other.DisplayName;
        }
    }
}