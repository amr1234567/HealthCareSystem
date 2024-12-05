namespace HSS.Domain.Models
{
    public class user_log
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public bool is_login { get; set; }
        public DateTime login_time { get; set; }
        public DateTime logout_time { get; set; }
        public string notes { get; set; }
    }
}
