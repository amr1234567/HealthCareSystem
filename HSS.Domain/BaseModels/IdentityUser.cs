using HSS.Domain.Models;
using HSS.Domain.Models.ManyToManyRelationEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.BaseModels
{
    public class IdentityUser<T> : BaseClass<T> where T : struct
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        public string Password { get; set; }

        public List<UserRole> Roles { get; set; }

        public List<UserLog> UserLogs { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid contact number.")]
        public string ContactNumber { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? ExpirationOfRefreshToken { get; set; }

        [Required]  // Ensures this field cannot be null
        public DateTime CreatedAt { get; set; }

        [Required]  // Ensures this field cannot be null
        public DateTime UpdatedAt { get; set; }
    }
}
