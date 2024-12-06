using HSS.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class ClinicAppointmentConfigurations : IEntityTypeConfiguration<ClinicAppointment>
    {
        public void Configure(EntityTypeBuilder<ClinicAppointment> builder)
        {
            builder.HasOne(b => b.Patient)
                .WithMany()
                .HasForeignKey(a => a.PatientId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Clinic)
                .WithMany()
                .HasForeignKey(a => a.ClinicId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Doctor)
               .WithMany()
               .HasForeignKey(a => a.DoctorId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.RadiologyAppointments)
              .WithOne(a => a.ClinicAppointmentRelatedTo)
              .HasForeignKey(a => a.ClinicAppointmentIdRelatedTo)
              .IsRequired(false)
              .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasMany(b => b.LabAppointments)
              .WithOne(a => a.ClinicAppointmentRelatedTo)
              .HasForeignKey(a => a.ClinicAppointmentIdRelatedTo)
              .IsRequired(false)
              .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
