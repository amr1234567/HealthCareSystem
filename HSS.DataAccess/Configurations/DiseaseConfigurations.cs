using HSS.Domain.Enums;
using HSS.Domain.Models;
using HSS.Domain.Models.ManyToManyRelationEntitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class DiseaseConfigurations : IEntityTypeConfiguration<Disease>
    {
        public void Configure(EntityTypeBuilder<Disease> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Severity)
               .HasConversion(
                       data => data.ToString(),
                       data => (Severity)Enum.Parse(typeof(Severity), data));
            builder.Property(b => b.CommonAgeGroup)
               .HasConversion(
                       data => data.ToString(),
                       data => (AgeGroup)Enum.Parse(typeof(AgeGroup), data));
            builder.Property(b => b.CommonGender)
               .HasConversion(
                       data => data.ToString(),
                       data => (Gender)Enum.Parse(typeof(Gender), data));
            builder.Property(b => b.ResearchStatus)
               .HasConversion(
                       data => data.ToString(),
                       data => (ResearchStatus)Enum.Parse(typeof(ResearchStatus), data));

            builder.HasMany<Symptom>(d => d.Symptoms)
                .WithMany()
                .UsingEntity<SymptomDisease>(join =>
                {
                    join.HasOne(j => j.Symptom)
                        .WithMany()
                        .HasForeignKey(join => join.SymptomId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                    join.HasOne(j => j.Disease)
                        .WithMany()
                        .HasForeignKey(join => join.DiseaseId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                    join.HasKey(j => new { j.SymptomId, j.DiseaseId });
                });
        }
    }
}
