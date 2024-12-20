using HSS.Domain.Models;

namespace HSS.Domain.IdentityModels
{
    public class Receptionist : IdentityUser
    {
        [DataType(DataType.Time)]
        public TimeSpan WorkingTime { get; set; }

        [Required]
        public int ReceptionId { get; set; }
        public Reception Reception { get; set; }
    }
}
