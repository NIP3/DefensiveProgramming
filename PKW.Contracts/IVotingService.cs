using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace PKW.Contracts
{
    [ServiceContract]
    public interface IVotingService
    {
        /// <summary>
        /// Operacja pobiera wszystkie dostępny okręgi wyborcze.
        /// </summary>
        /// <returns>Dostępne okręgi wyborcze</returns>
        [OperationContract]
        IEnumerable<Constituency> GetConstituencies();

        /// <summary>
        /// Operacja pobiera wszystkich kandydatów startujących w wyborach.
        /// </summary>
        /// <returns>Wszystkich kandydatów startujących w wyborach.</returns>
        [OperationContract]
        IEnumerable<Candidate> GetCandidates();

        /// <summary>
        /// Operacja pobiera kandydatów startujących w ramach jednego okręgu wyborczego.
        /// </summary>
        /// <param name="constituencyId">Id okręg wyborczy, dla którego mają zostać wyszukani startujący kandydaci.</param>
        /// <returns>Lista kandydatów startujących w zadanym okręgu wyborczym.</returns>
        [OperationContract]
        IEnumerable<Candidate> GetConstituencyCandidates(int constituencyId);

        /// <summary>
        /// Przesłanie podliczonych wyników wyborów w danym okręgu wyborczym.
        /// </summary>
        /// <param name="votesReport">Protokół komisji wyborczej – podliczone głosy w okręgu</param>
        [OperationContract]
        void SendVotes(VotesReport votesReport);

        /// <summary>
        /// Pobranie wyników głosowania na poszczególnych kandydatów.
        /// </summary>
        /// <param name="constituencyId">
        /// Wyniki głosowania może zostać zwrócony w ramach zadanego okręgu wyborczego (id).
        /// Jeśli constituencyId = null, wtedy zwracane są wyniki głosowania w ramach wszystkich okręgów wyborczych.
        /// </param>
        /// <returns>Wyniki głosowania</returns>
        [OperationContract]
        VotingSummary GetVotingSummary(int? constituencyId);
    }
}