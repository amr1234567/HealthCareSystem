﻿using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class RadiologyCenterConfigurations : IEntityTypeConfiguration<RadiologyCenter>
    {
        public void Configure(EntityTypeBuilder<RadiologyCenter> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);
            builder.HasOne(u => u.Hospital)
              .WithMany()
              .HasForeignKey(u => u.HospitalId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();
        }
    }
}
