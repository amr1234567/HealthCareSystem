using HSS.Domain.Models;
using HSS.Domain.Models.ManyToManyRelationEntitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Domain.BaseModels
{
    public class IdentityUser : BaseClass<int> 
    {
       [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(14)]
        public string NationalId { get; set; }

        [AllowNull]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [AllowNull]
        public string? Password { get; set; }

        [AllowNull]
        public string? Salt { get; set; }

        public List<Role> Roles { get; set; }

        public List<UserLog>? UserLogs { get; set; }

        [AllowNull]
        [Phone(ErrorMessage = "Invalid contact number.")]
        public string? ContactNumber { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? ExpirationOfRefreshToken { get; set; }

        [Required]  // Ensures this field cannot be null
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [AllowNull]  // Ensures this field cannot be null
        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }
    }
}
