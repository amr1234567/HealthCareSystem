using HSS.Domain.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class PharmacistConfigurations : IEntityTypeConfiguration<Pharmacist>
    {
        public void Configure(EntityTypeBuilder<Pharmacist> builder)
        {
            builder.HasOne(b => b.Pharmacy)
                .WithMany(p => p.Pharmacists)
                .HasForeignKey(a => a.PharmacyId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
