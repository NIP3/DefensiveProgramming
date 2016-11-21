using System.Linq;
using PKW.ControlCenter.Data;

namespace PKW.ControlCenter.Voting
{
    public interface IDataRepository
    {
        void Add(CandidatesModel candidate);
        CandidatesModel GetCandidate(int candidateId);
        IQueryable<CandidatesModel> GetCandidates();
        bool CandidateExists(int candidateId);

        void Add(ConstituencyModel constituency);
        ConstituencyModel GetConstituence(int constituenceId);
        IQueryable<ConstituencyModel> GetConstituencies();
        bool ConstituenceExists(int constituenceId);
    }
}