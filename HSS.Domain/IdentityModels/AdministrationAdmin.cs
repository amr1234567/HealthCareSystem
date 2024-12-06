using HSS.Domain.Models;

namespace HSS.Domain.IdentityModels
{
    public class AdministrationAdmin : IdentityUser<int>
    {
        [Required]  // Ensures this field cannot be null
        public int AdministrationId { get; set; }

        public Administration Administration { get; set; }
    }

}
