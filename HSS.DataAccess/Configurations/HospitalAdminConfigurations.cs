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
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(b => b.Hospital)
                .WithOne(b => b.HospitalAdmin)
                .IsRequired(false)
                .HasForeignKey<HospitalAdmin>(a => a.HospitalId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
