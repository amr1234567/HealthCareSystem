using HSS.Domain.BaseModels;
using HSS.Domain.Models;
using HSS.Domain.Models.ManyToManyRelationEntitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.DataAccess.Configurations
{
    public class IdentityUserConfigurations : IEntityTypeConfiguration<IdentityUser<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUser<int>> builder)
        {
            builder.Property(b => b.Id).IsRequired();
            builder.HasKey(b => b.Id);
            builder.HasMany<Role>()
                .WithMany()
                .UsingEntity<UserRole>(join =>
                {
                    join.HasOne(j => j.User)
                        .WithMany(u => u.Roles)
                        .HasForeignKey(u => u.UserId)
                        .OnDelete(DeleteBehavior.Cascade);
                    join.HasOne(j => j.Role)
                        .WithMany()
                        .HasForeignKey(u => u.RoleId)
                        .OnDelete(DeleteBehavior.Cascade);
                    join.HasKey(j => new { j.UserId, j.RoleId });
                });
            builder.HasMany(u => u.UserLogs)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
