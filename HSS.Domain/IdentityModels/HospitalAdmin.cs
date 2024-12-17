using HSS.Domain.Models;

namespace HSS.Domain.IdentityModels
{
    public class HospitalAdmin : IdentityUser
    {
        [Required]
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
    }
}
