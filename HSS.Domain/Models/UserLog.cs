namespace HSS.Domain.Models
{
    public class UserLog : BaseClass<int>
    {
        [Required]  // Ensures this field cannot be null
        public int UserId { get; set; }
        public IdentityUser User { get; set; }

        [Required]  // Ensures this field is mandatory
        public bool IsLoggedIn { get; set; }

        [Required]  // Ensures this field is mandatory
        [DataType(DataType.DateTime)]
        public DateTime LoginTime { get; set; }

        [AllowNull]  // Ensures this field cannot be null
        [DataType(DataType.DateTime)]
        public DateTime? LogoutTime { get; set; }

        [AllowNull, StringLength(500, ErrorMessage = "Notes cannot be longer than 500 characters.")]
        public string? Notes { get; set; }
    }
}
