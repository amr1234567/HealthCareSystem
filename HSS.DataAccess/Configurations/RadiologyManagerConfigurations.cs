using HSS.Domain.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class RadiologyManagerConfigurations : IEntityTypeConfiguration<RadiologyManager>
    {
        public void Configure(EntityTypeBuilder<RadiologyManager> builder)
        {
            builder.HasOne(b => b.RadiologyCenter)
                .WithMany(p => p.RadiologyManagers)
                .HasForeignKey(a => a.RadiologyCenterId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
