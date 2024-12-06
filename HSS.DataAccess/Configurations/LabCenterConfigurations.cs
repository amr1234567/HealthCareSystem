using HSS.Domain.Models;
using HSS.Domain.Models.ManyToManyRelationEntitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class LabCenterConfigurations : IEntityTypeConfiguration<LabCenter>
    {
        public void Configure(EntityTypeBuilder<LabCenter> builder)
        {
            builder.HasMany<LabCenterTest>()
                .WithMany()
                .UsingEntity<LabCenterLabTest>(join =>
                {
                    join.HasOne(j => j.LabCenter)
                        .WithMany()
                        .HasForeignKey(join => join.LabCenterId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                    join.HasOne(j => j.LabCenterTest)
                        .WithMany()
                        .HasForeignKey(join => join.LabTestId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                    join.HasKey(j => new { j.LabTestId, j.LabCenterId });
                });
        }
    }
}
