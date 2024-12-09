using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class PharmacyConfigurations : IEntityTypeConfiguration<Pharmacy>
    {
        public void Configure(EntityTypeBuilder<Pharmacy> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(u => u.Hospital)
              .WithMany()
              .HasForeignKey(u => u.HospitalId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();
        }
    }
}
