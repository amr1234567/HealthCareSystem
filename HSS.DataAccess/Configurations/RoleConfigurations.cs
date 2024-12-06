using HSS.Domain.Enums;
using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(b => b.RoleName)
              .HasConversion(
                      data => data.ToString(),
                      data => (ApplicationRole)Enum.Parse(typeof(ApplicationRole), data));
        }
    }
}
