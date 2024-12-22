using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class MedicalHistoryConfigurations : IEntityTypeConfiguration<MedicalHistory>
    {
        public void Configure(EntityTypeBuilder<MedicalHistory> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);
            builder.HasOne(j => j.Patient)
                    .WithMany()
                    .HasForeignKey(join => join.PatientId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
