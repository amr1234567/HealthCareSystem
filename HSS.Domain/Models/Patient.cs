namespace HSS.Domain.Models
{
    public class Patient
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public DateTime date_of_birth { get; set; }
        public int sex { get; set; }
        public int age_category { get; set; }
        public int education_level { get; set; }
        public int income_category { get; set; }
        public int diabetes_binary { get; set; }
        public int high_bp { get; set; }
        public int high_chol { get; set; }
        public int chol_check { get; set; }
        public float bmi { get; set; }
        public int smoker { get; set; }
        public int stroke { get; set; }
        public int heart_disease_or_attack { get; set; }
        public int phys_activity { get; set; }
        public int fruits { get; set; }
        public int veggies { get; set; }
        public int hvy_alcohol_consump { get; set; }
        public int any_healthcare { get; set; }
        public int no_docbc_cost { get; set; }
        public int gen_hlth { get; set; }
        public int ment_hlth { get; set; }
        public int phys_hlth { get; set; }
        public int diff_walk { get; set; }
        public DateTime last_visit_date { get; set; }
        public string contact_number { get; set; }
        public string email { get; set; }
        public string address { get; set; }
    }
}
