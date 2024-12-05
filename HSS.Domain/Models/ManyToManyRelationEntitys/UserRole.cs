namespace HSS.Domain.Models.ManyToManyRelationEntitys
{
    public class UserRole
    {
        [Required]
        public int UserId { get; set; }
        public IdentityUser<int> User { get; set; }

        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
