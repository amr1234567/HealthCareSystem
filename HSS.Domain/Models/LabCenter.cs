﻿using HSS.Domain.IdentityModels;
using HSS.Domain.Models.ManyToManyRelationEntitys;

namespace HSS.Domain.Models
{
    public class LabCenter : BaseClass<int>
    {
        [Required]
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }

        [Required, MaxLength(500)]
        public string Location { get; set; }

        public List<LabCenter> TestsAvailable { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan StartAt { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan EndAt { get; set; }

        [Required, Range(5, 90)]
        public int AppointmentDuration { get; set; }

        public List<LabManager> labManagers { get; set; }
    }
}
