using HSS.Domain.IdentityModels;
using HSS.Domain.Models;
using HSS.Domain.Models.ManyToManyRelationEntitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class HospitalConfigurations : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(b => b.HospitalAdmin)
                .WithOne(b => b.Hospital)
                .IsRequired(true)
                .HasForeignKey<Hospital>(a => a.HospitalAdminId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany<ClinicSpecialization>(h => h.ClinicSpecializations)
                .WithMany(s => s.Hospitals)
                .UsingEntity<ClinicSpecializationHospital>(join =>
                {
                    join.HasOne(j => j.ClinicSpecialization)
                        .WithMany()
                        .HasForeignKey(join => join.ClinicSpecializationId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                    join.HasOne(j => j.Hospital)
                        .WithMany()
                        .HasForeignKey(join => join.HospitalId)
                        .IsRequired()
                        .OnDelete(DeleteBehavior.Restrict);
                    join.HasKey(j => new { j.HospitalId, j.ClinicSpecializationId });
                });
        }
    }
}
