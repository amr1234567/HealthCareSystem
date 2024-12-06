using HSS.Domain.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class AdministrationAdminConfigurations : IEntityTypeConfiguration<AdministrationAdmin>
    {
        public void Configure(EntityTypeBuilder<AdministrationAdmin> builder)
        {
            builder.HasOne(b => b.Administration)
                .WithOne()
                .HasForeignKey<AdministrationAdmin>(a => a.AdministrationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
