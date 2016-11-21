using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PKW.Contracts;
using PKW.ControlCenter.Data;

namespace PKW.ControlCenter
{
    public class VotingService : IVotingService
    {
        private readonly IDataRepository _repository;


        public VotingService(IDataRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Constituency> GetConstituencies()
        {
            var constituencies = _repository.GetConstituencies()
                .Select(constituency => new Constituency() { Id = constituency.Id, Name = constituency.Name })
                .ToList();

            return constituencies;
        }

        public IEnumerable<Candidate> GetCandidates()
        {
            var candidates = _repository.GetCandidates()
                .Select(candidate => new Candidate() { Id = candidate.Id, DisplayName = candidate.Name })
                .ToList();

            return candidates;
        }

        public void SendVotes(VotesReport votesReport)
        {
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

        public IEnumerable<CandidateVotes> GetCandidateVotes()
        {
            var query = from vote in _repository.GetConstituencies().Where(c => c.Votes != null).SelectMany(c => c.Votes)
                        where vote.Key != null
                        group vote by vote.Key
                            into grp
                            select new CandidateVotes()
                            {
                                CandidateId = grp.Key.Id,
                                CandidateName = grp.Key.Name,
                                Amount = grp.Sum(g => g.Value)
                            };

            var candidateVotes = query.ToList();

            return candidateVotes;
        }
    }
}