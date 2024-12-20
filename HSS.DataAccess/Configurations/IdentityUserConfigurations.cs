using HSS.Domain.BaseModels;
using HSS.Domain.Models;
using HSS.Domain.Models.ManyToManyRelationEntitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class IdentityUserConfigurations : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            builder.Property(b => b.Id).IsRequired();
            builder.HasKey(b => b.Id);

            builder.HasMany<Role>(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRole>(join =>
                {
                    join.HasOne(j => j.User)
                        .WithMany()
                        .HasForeignKey(u => u.UserId)
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                    join.HasOne(j => j.Role)
                        .WithMany()
                        .HasForeignKey(u => u.RoleId)
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                    join.HasKey(j => new { j.UserId, j.RoleId });
                });

            builder.HasMany(u => u.UserLogs)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
    
}
