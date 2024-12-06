using HSS.Domain.Enums;
using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class SymptomConfigurations : IEntityTypeConfiguration<Symptom>
    {
        public void Configure(EntityTypeBuilder<Symptom> builder)
        {
            builder.Property(b => b.Age)
             .HasConversion(
                     data => data.ToString(),
                     data => (AgeGroup)Enum.Parse(typeof(AgeGroup), data));
            builder.Property(b => b.Severity)
              .HasConversion(
                      data => data.ToString(),
                      data => (Severity)Enum.Parse(typeof(Severity), data));
            builder.Property(b => b.OnsetPattern)
              .HasConversion(
                      data => data.ToString(),
                      data => (SymptomOnsetPattern)Enum.Parse(typeof(SymptomOnsetPattern), data));
        }
    }
}
