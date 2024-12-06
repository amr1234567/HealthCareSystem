namespace HSS.Domain.Models
{
    using HSS.Domain.IdentityModels;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Pharmacy : BaseClass<int>
    {
        [Required]
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }

        [Required]
        [StringLength(400)]
        public string Location { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan StartAt { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan EndAt { get; set; }

        [Required]
        [Phone]
        public string ContactNumber { get; set; }

        //[StringLength(500)]
        //public string ServicesProvided { get; set; }

        public List<Pharmacist> Pharmacists { get; set; }

    }

}
