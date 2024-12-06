﻿using HSS.Domain.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class DoctorConfigurations : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasOne(b => b.Hospital)
                .WithMany()
                .HasForeignKey(a => a.HospitalId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.Clinic)
               .WithMany()
               .HasForeignKey(a => a.ClinicId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}