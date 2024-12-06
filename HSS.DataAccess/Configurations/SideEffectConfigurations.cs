using HSS.Domain.Enums;
using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class SideEffectConfigurations : IEntityTypeConfiguration<SideEffect>
    {
        public void Configure(EntityTypeBuilder<SideEffect> builder)
        {
            builder.Property(b => b.AgeRange)
              .HasConversion(
                      data => data.ToString(),
                      data => (AgeGroup)Enum.Parse(typeof(AgeGroup), data));
            builder.Property(b => b.Severity)
              .HasConversion(
                      data => data.ToString(),
                      data => (Severity)Enum.Parse(typeof(Severity), data));
            builder.Property(b => b.Commonality)
              .HasConversion(
                      data => data.ToString(),
                      data => (SideEffectCommonality)Enum.Parse(typeof(SideEffectCommonality), data));
        }
    }
}
