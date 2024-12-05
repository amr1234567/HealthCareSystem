namespace HSS.Domain.Models
{
    public class EffectiveSubstance
    {
        public int id { get; set; }
        public string name { get; set; }
        public string chemical_formula { get; set; }
        public string description { get; set; }
        public DateTime discovery_date { get; set; }
        public string approved_by { get; set; }
        public string stability_conditions { get; set; }
        public string side_effects { get; set; }
        public string primary_usage { get; set; }
        public string alternative_names { get; set; }
    }
}
