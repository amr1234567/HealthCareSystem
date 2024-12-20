using HSS.Domain.Enums;
using HSS.Domain.Models;
using HSS.Domain.Models.ManyToManyRelationEntitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class MedicineConfigurations : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(b => b.Type)
              .HasConversion(
                      data => data.ToString(),
                      data => (MedicinesType)Enum.Parse(typeof(MedicinesType), data));
            builder.HasMany<SideEffect>(m => m.SideEffects)
               .WithMany(s => s.Medicines)
               .UsingEntity<SideEffectMedicine>(join =>
               {
                   join.HasOne(j => j.SideEffect)
                       .WithMany()
                       .HasForeignKey(join => join.SideEffectId)
                       .IsRequired()
                       .OnDelete(DeleteBehavior.Restrict);
                   join.HasOne(j => j.Medicine)
                       .WithMany()
                       .HasForeignKey(join => join.MedicineId)
                       .IsRequired()
                       .OnDelete(DeleteBehavior.Restrict);
                   join.HasKey(j => new { j.MedicineId, j.SideEffectId });
               });
        }
    }
}
