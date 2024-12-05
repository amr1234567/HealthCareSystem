namespace HSS.Domain.Models.ManyToManyRelationEntitys
{
    public class ClinicSpecializationHospital
    {
        [Required]
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }

        [Required]
        public int ClinicSpecializationId { get; set; }
        public ClinicSpecialization ClinicSpecialization { get; set; }
    }
}