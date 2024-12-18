﻿using HSS.Domain.Models;

namespace HSS.Domain.IdentityModels
{
    public class HospitalAdmin : IdentityUser<int>
    {
        [Required]
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
    }
}
