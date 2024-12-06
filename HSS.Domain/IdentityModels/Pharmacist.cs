using HSS.Domain.Models;

namespace HSS.Domain.IdentityModels
{
    public class Pharmacist : IdentityUser<int>
    {
        [Required]
        [DataType(DataType.Time)]
        public TimeSpan WorkingTime { get; set; }

        public Pharmacy Pharmacy { get; set; }
        [Required]
        public int PharmacyId { get; set; }

    }
}