using HSS.Domain.IdentityModels;

namespace HSS.Domain.Models
{
    public class Administration : BaseClass<int>
    {
        public AdministrationAdmin Admin { get; set; }
    }
}