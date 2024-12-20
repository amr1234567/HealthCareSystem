using HSS.Domain.Models;

namespace HSS.Services.Models
{
    public class SpecializationDto
    {
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }
        public SpecializationDto(ClinicSpecialization specialization)
        {
            SpecializationId = specialization.Id;
            SpecializationName = specialization.Name;

        }
    }
}
