using HSS.Domain.IdentityModels;
using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class HospitalAdminConfigurations : IEntityTypeConfiguration<HospitalAdmin>
    {
        public void Configure(EntityTypeBuilder<HospitalAdmin> builder)
        {
            builder.HasOne(b => b.Hospital)
                .WithOne()
                .HasForeignKey<HospitalAdmin>(a => a.HospitalId)
                .HasForeignKey<Hospital>(a => a.HospitalAdminId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
