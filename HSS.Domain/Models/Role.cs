namespace HSS.Domain.Models
{
    public class Role : BaseClass<int>
    {
        [Required]
        public ApplicationRole RoleName { get; set; }
    }
}
