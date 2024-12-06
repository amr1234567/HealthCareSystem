using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class ClinicConfigurations : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.HasOne(u => u.Hospital)
                .WithMany()
                .HasForeignKey(u => u.HospitalId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.HasOne(u => u.Specialization)
               .WithMany()
               .HasForeignKey(u => u.SpecializationId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();
        }
    }
}
