namespace HSS.Domain.Models
{
    public class SideEffect
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string age_range { get; set; }
        public string severity { get; set; }
        public string commonality { get; set; }
        public string duration { get; set; }
        public bool reversibility { get; set; }
        public string affected_system { get; set; }
        public string notes { get; set; }
    }
}
