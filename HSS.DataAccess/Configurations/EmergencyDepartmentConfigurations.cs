using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class EmergencyDepartmentConfigurations : IEntityTypeConfiguration<EmergencyDepartment>
    {
        public void Configure(EntityTypeBuilder<EmergencyDepartment> builder)
        {
            builder.HasKey(x => x.Id);

        }
    }
}
