using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PKW.ControlCenter.Data
{
    public class ConstituencyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InvalidVotes { get; set; }
        public int IssuedBallots { get; set; }
        public Dictionary<CandidatesModel, int> Votes { get; set; }
    }
}