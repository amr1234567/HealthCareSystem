namespace HSS.Domain.Models
{
    public class UserLog : BaseClass<int>
    {
        [Required]  // Ensures this field cannot be null
        public int UserId { get; set; }
        public IdentityUser<int> User { get; set; }

        [Required]  // Ensures this field is mandatory
        public bool IsLogin { get; set; }

        [Required]  // Ensures this field is mandatory
        public DateTime LoginTime { get; set; }

        [Required]  // Ensures this field cannot be null
        public DateTime LogoutTime { get; set; }

        [StringLength(500, ErrorMessage = "Notes cannot be longer than 500 characters.")]
        public string Notes { get; set; }
    }
}
