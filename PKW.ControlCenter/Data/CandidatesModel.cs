namespace PKW.ControlCenter.Data
{
    public class CandidatesModel
    {
        public CandidatesModel()
        {
            ConstituencyId = 0;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ConstituencyId { get; set; }
    }
}