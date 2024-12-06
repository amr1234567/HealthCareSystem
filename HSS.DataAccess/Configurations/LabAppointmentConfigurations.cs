using HSS.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class LabAppointmentConfigurations : IEntityTypeConfiguration<LabAppointment>
    {
        public void Configure(EntityTypeBuilder<LabAppointment> builder)
        {
            builder.HasOne(b => b.LabCenter)
                .WithMany()
                .HasForeignKey(a => a.LabCenterId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.LabManager)
                .WithMany()
                .HasForeignKey(a => a.LabTesterId)
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
