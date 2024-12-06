using HSS.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class RadiologyAppointmentConfigurations : IEntityTypeConfiguration<RadiologyAppointment>
    {
        public void Configure(EntityTypeBuilder<RadiologyAppointment> builder)
        {
            builder.HasOne(b => b.RadiologyCenter)
                .WithMany()
                .HasForeignKey(a => a.RadiologyCenterId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.RadiologyManager)
                .WithMany()
                .HasForeignKey(a => a.RadiologyTesterId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.TestType)
                .WithMany()
                .HasForeignKey(a => a.TestTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
