using HSS.Domain.Enums;
using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class PrescriptionRecordConfigurations : IEntityTypeConfiguration<PrescriptionRecord>
    {
        public void Configure(EntityTypeBuilder<PrescriptionRecord> builder)
        {
            builder.HasKey(b => b.Id);
            builder.HasOne(b => b.Medicine)
                .WithMany()
                .HasForeignKey(a => a.MedicineId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.ClinicAppointment)
              .WithMany()
              .HasForeignKey(a => a.ClinicAppointmentId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

            builder.Property(b => b.MedicineUnitType)
               .HasConversion(
                       data => data.ToString(),
                       data => (MedicineUnitType)Enum.Parse(typeof(MedicineUnitType), data));

            builder.Property(b => b.DosageFrequency)
               .HasConversion(
                       data => data.ToString(),
                       data => (DosageFrequency)Enum.Parse(typeof(DosageFrequency), data));

            builder.Property(b => b.DispenseStatus)
               .HasConversion(
                       data => data.ToString(),
                       data => (DispenseStatus)Enum.Parse(typeof(DispenseStatus), data));
        }
    }
}
