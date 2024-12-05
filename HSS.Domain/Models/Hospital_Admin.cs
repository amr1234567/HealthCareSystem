﻿namespace HSS.Domain.Models
{
    public class Hospital_Admin
    {
        public int id { get; set; }
        public int hospital_id { get; set; }
        public int user_id { get; set; }
        public string full_name { get; set; }
        public string contact_number { get; set; }
        public string email { get; set; }
        public string role { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
