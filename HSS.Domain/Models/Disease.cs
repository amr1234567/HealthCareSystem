namespace HSS.Domain.Models
{
    public class Disease
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string severity { get; set; }
        public bool contagious { get; set; }
        public DateTime discovery_date { get; set; }
        public int affected_population { get; set; }
        public string disease_code { get; set; }
        public float cure_rate { get; set; }
        public float fatality_rate { get; set; }
        public string treatment_duration { get; set; }
        public bool is_chronic { get; set; }
        public bool has_vaccine { get; set; }
        public string common_age_group { get; set; }
        public string common_gender { get; set; }
        public string risk_factors { get; set; }
        public string geographic_spread { get; set; }
        public DateTime last_outbreak_date { get; set; }
        public string research_status { get; set; }
        public string prevention_measures { get; set; }
        public string related_diseases { get; set; }
        public string notes { get; set; }
    }
}
