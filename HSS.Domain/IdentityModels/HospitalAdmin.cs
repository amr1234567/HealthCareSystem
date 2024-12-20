using HSS.Domain.Models;

namespace HSS.Domain.IdentityModels
{
    public class HospitalAdmin : IdentityUser
    {
        public Hospital? Hospital { get; set; }
        public int? HospitalId { get; set; }
    }
}
