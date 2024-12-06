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
            builder.HasMany<SideEffect>()
                .WithMany()
                .UsingEntity<SideEffectEffectiveSubstance>(join =>
                {
                    join.HasOne(j => j.SideEffect)
                        .WithMany()
                        .HasForeignKey(join => join.SideEffectId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                    join.HasOne(j => j.EffectiveSubstance)
                        .WithMany()
                        .HasForeignKey(join => join.EffectiveSubstanceId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                    join.HasKey(j => new { j.SideEffectId, j.EffectiveSubstanceId });
                });
        }
    }
}
