﻿using HSS.Domain.IdentityModels;
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
            builder.HasMany<ClinicSpecialization>()
                .WithMany()
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
