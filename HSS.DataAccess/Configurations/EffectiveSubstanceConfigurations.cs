using HSS.Domain.Models;
using HSS.Domain.Models.ManyToManyRelationEntitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class EffectiveSubstanceConfigurations : IEntityTypeConfiguration<EffectiveSubstance>
    {
        public void Configure(EntityTypeBuilder<EffectiveSubstance> builder)
        {
            builder.HasKey(x => x.Id); builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasMany<SideEffect>(e => e.SideEffects)
               .WithMany(s => s.EffectiveSubstances)
               .UsingEntity<SideEffectEffectiveSubstance>(
                  join => join.HasOne(j => j.SideEffect)
                       .WithMany()
                       .HasForeignKey(join => join.SideEffectId)
                       .IsRequired()
                       .OnDelete(DeleteBehavior.Restrict),
                  join => join.HasOne(j => j.EffectiveSubstance)
                       .WithMany()
                       .HasForeignKey(join => join.EffectiveSubstanceId)
                       .IsRequired()
                       .OnDelete(DeleteBehavior.Restrict),
                  join => join.HasKey(j => new { j.SideEffectId, j.EffectiveSubstanceId }));


            builder.HasMany<Medicine>(e => e.Medicines)
                .WithMany(m => m.EffectiveSubstances)
                .UsingEntity<EffectiveSubstanceMedicine>(
                   join => join.HasOne(j => j.Medicine)
                        .WithMany()
                        .HasForeignKey(join => join.MedicineId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict),
                   join => join.HasOne(j => j.EffectiveSubstance)
                        .WithMany()
                        .HasForeignKey(join => join.EffectiveSubstanceId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict),
                   join => join.HasKey(j => new { j.MedicineId, j.EffectiveSubstanceId }));

            builder.HasMany<Disease>()
               .WithMany()
               .UsingEntity<EffectiveSubstanceDisease>(
                  join => join.HasOne(j => j.Disease)
                       .WithMany()
                       .HasForeignKey(join => join.DiseaseId)
                       .IsRequired()
                       .OnDelete(DeleteBehavior.Restrict),
                  join => join.HasOne(j => j.EffectiveSubstance)
                       .WithMany()
                       .HasForeignKey(join => join.EffectiveSubstanceId)
                       .IsRequired()
                       .OnDelete(DeleteBehavior.Restrict),
                  join => join.HasKey(j => new { j.DiseaseId, j.EffectiveSubstanceId }));

        }
    }
}
