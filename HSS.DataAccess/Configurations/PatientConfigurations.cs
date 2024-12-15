using HSS.Domain.Enums;
using HSS.Domain.IdentityModels;
using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class PatientConfigurations : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasOne(b => b.PatientMediacalDetails)
                .WithOne(b => b.Patient)
                .HasForeignKey<Patient>(a => a.PatientMediacalDetailsId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(b => b.AgeCategory)
                .HasConversion(
                        data => data.ToString(),
                        data => (AgeGroup)Enum.Parse(typeof(AgeGroup), data));
            builder.Property(b => b.EducationLevel)
                .HasConversion(
                        data => data.ToString(),
                        data => (EducationLevel)Enum.Parse(typeof(EducationLevel), data));
            builder.Property(b => b.IncomeCategory)
                .HasConversion(
                        data => data.ToString(),
                        data => (IncomeCategory)Enum.Parse(typeof(IncomeCategory), data));
            builder.Property(b => b.Sex)
                .HasConversion(
                        data => data.ToString(),
                        data => (Gender)Enum.Parse(typeof(Gender), data));
            builder.ComplexProperty(b => b.Address, a =>
            {

            });
        }
    }
}
