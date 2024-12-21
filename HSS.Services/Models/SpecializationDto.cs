using HSS.Domain.Models;

namespace HSS.Services.Models
{
    public class SpecializationDto
    {
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }
        public string SpecializationDescription { get; set; }
        public string SpecializationIcon { get; set; }
        public SpecializationDto(ClinicSpecialization specialization)
        {
            SpecializationId = specialization.Id;
            SpecializationName = specialization.Name;
            SpecializationDescription = specialization.Description;
            SpecializationIcon = specialization.Icon;
        }
    }
}
