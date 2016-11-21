using System.Collections.Generic;
using System.Linq;
using PKW.ControlCenter.Data;

namespace PKW.ControlCenter.Voting
{
    public class DataRepository : IDataRepository
    {
        private readonly Dictionary<int, CandidatesModel> _candidates;
        private readonly Dictionary<int, ConstituencyModel> _constituenies;

        public DataRepository()
        {
            _candidates = new Dictionary<int, CandidatesModel>();
            _constituenies = new Dictionary<int, ConstituencyModel>();
        }

        public void Add(CandidatesModel candidate)
        {
            _candidates.Add(candidate.Id, candidate);
        }

        public CandidatesModel GetCandidate(int candidateId)
        {
            return _candidates[candidateId];
        }

        public bool CandidateExists(int candidateId)
        {
            return _candidates.ContainsKey(candidateId);
        }

        public IQueryable<CandidatesModel> GetCandidates()
        {
            return _candidates.Values.AsQueryable();
        }

        public void Add(ConstituencyModel constituency)
        {
            _constituenies.Add(constituency.Id, constituency);
        }

        public ConstituencyModel GetConstituence(int constituenceId)
        {
            return _constituenies[constituenceId];
        }

        public bool ConstituenceExists(int constituenceId)
        {
            return _constituenies.ContainsKey(constituenceId);
        }

        public IQueryable<ConstituencyModel> GetConstituencies()
        {
            return _constituenies.Values.AsQueryable();
        }
    }
}