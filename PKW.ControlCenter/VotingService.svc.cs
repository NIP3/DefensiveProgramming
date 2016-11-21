using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PKW.Contracts;
using PKW.ControlCenter.Data;
using PKW.ControlCenter.Voting;

namespace PKW.ControlCenter
{
    public class VotingService : IVotingService
    {
        private readonly IDataRepository _repository;

        public VotingService(IDataRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Operacja pobiera wszystkie dostępny okręgi wyborcze.
        /// </summary>
        /// <returns>Dostępne okręgi wyborcze</returns>
        public IEnumerable<Constituency> GetConstituencies()
        {
            var constituencies = _repository.GetConstituencies()
                .Select(constituency => new Constituency() { Id = constituency.Id, Name = constituency.Name })
                .ToList();

            return constituencies;
        }

        /// <summary>
        /// Operacja pobiera kandydatów startujących w ramach jednego okręgu wyborczego.
        /// </summary>
        /// <param name="constituencyId">Id okręg wyborczy, dla którego mają zostać wyszukani startujący kandydaci.</param>
        /// <returns>Lista kandydatów startujących w zadanym okręgu wyborczym.</returns>
        public IEnumerable<Candidate> GetConstituencyCandidates(int constituencyId)
        {
            if (!_repository.ConstituenceExists(constituencyId))
            {
                throw new ArgumentException(string.Format("Nie istnieje okręg wyborczy o ID {0}",constituencyId));
            }

            return _repository.GetCandidates()
                    .Where(x => x.ConstituencyId == constituencyId)
                    .Select(x => new Candidate() { ConstituencyId = x.ConstituencyId, DisplayName = x.Name, Id = x.Id });
        }

        /// <summary>
        /// Operacja pobiera wszystkich kandydatów startujących w wyborach.
        /// </summary>
        /// <returns>Wszystkich kandydatów startujących w wyborach.</returns>
        public IEnumerable<Candidate> GetCandidates()
        {
            var candidates = _repository.GetCandidates()
                .Select(candidate => new Candidate() { Id = candidate.Id, DisplayName = candidate.Name })
                .ToList();

            return candidates;
        }

        /// <summary>
        /// Przesłanie podliczonych wyników wyborów w danym okręgu wyborczym.
        /// </summary>
        /// <param name="votesReport">Protokół komisji wyborczej – podliczone głosy w okręgu</param>
        public void SendVotes(VotesReport votesReport)
        {
            // Defensive Lab: Weryfikacja poprawnośći przesłanych danych
                        
            var constituency = _repository.GetConstituence(votesReport.ConstituencyId);

            Dictionary<CandidatesModel, int> votesSummary = new Dictionary<CandidatesModel, int>();

            foreach (var candidate in votesReport.Votes)
            {
                CandidatesModel condidate = _repository.GetCandidate(candidate.CandidateId);
                votesSummary[condidate] = candidate.Amount;
            }

            constituency.InvalidVotes = votesReport.InvalidVotes;
            constituency.IssuedBallots = votesReport.IssuedBallots;
            constituency.Votes = votesSummary;
        }

        /// <summary>
        /// Pobranie wyników głosowania na poszczególnych kandydatów.
        /// </summary>
        /// <param name="constituencyId">
        /// Wyniki głosowania może zostać zwrócony w ramach zadanego okręgu wyborczego (id).
        /// Jeśli constituencyId = null, wtedy zwracane są wyniki głosowania w ramach wszystkich okręgów wyborczych.
        /// </param>
        /// <returns>Wyniki głosowania</returns>
        public VotingSummary GetVotingSummary(int? constituencyId)
        {
            IQueryable<ConstituencyModel> constituencies = null;

            if (constituencyId.HasValue)
            {
                List<ConstituencyModel> cm = new List<ConstituencyModel>();
                cm.Add(_repository.GetConstituence(constituencyId.Value));
                constituencies = cm.AsQueryable();
            }
            else
            {
                constituencies = _repository.GetConstituencies();
            }

            var allVotes = constituencies.Where(c => c.Votes != null)
                .SelectMany(c => c.Votes);

            int totalValidVoteCount = allVotes.Sum(v => v.Value);

            var agreagatedVotes = allVotes.GroupBy(v => v.Key)
                .Select(g => new CandidateVotes()
                {
                    CandidateId = g.Key.Id,
                    CandidateName = g.Key.Name,
                    Amount = g.Sum(c => c.Value),
                    Percentage = (double)g.Sum(c => c.Value) / totalValidVoteCount
                });

            return new VotingSummary()
            {
                InvalidVotes = constituencies.Sum(c => c.InvalidVotes),
                IssuedBallots = constituencies.Sum(c => c.IssuedBallots),
                AggregatedVoteses = agreagatedVotes.ToArray()
            };
        }
    }
}


