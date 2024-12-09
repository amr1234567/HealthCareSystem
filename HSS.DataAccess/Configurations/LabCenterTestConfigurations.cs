using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class LabCenterTestConfigurations : IEntityTypeConfiguration<LabCenterTest>
    {
        public void Configure(EntityTypeBuilder<LabCenterTest> builder)
        {
            builder.HasKey(x => x.Id);

        }
    }
}
