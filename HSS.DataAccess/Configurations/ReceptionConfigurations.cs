﻿using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class ReceptionConfigurations : IEntityTypeConfiguration<Reception>
    {
        public void Configure(EntityTypeBuilder<Reception> builder)
        {
            builder.HasOne(u => u.Hospital)
              .WithMany()
              .HasForeignKey(u => u.HospitalId)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();
        }
    }
}
