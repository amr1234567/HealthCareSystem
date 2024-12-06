using HSS.Domain.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class LabManagerConfigurations : IEntityTypeConfiguration<LabManager>
    {
        public void Configure(EntityTypeBuilder<LabManager> builder)
        {
            builder.HasOne(b => b.LabCenter)
                .WithMany(b => b.labManagers)
                .HasForeignKey(a => a.LabCenterId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
