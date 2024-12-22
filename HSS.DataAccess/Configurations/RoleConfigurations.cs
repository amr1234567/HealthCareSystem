using HSS.Domain.BaseModels;
using HSS.Domain.Enums;
using HSS.Domain.Models;
using HSS.Domain.Models.ManyToManyRelationEntitys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasKey(x => x.Id);
            builder.Property(b => b.RoleName)
              .HasConversion(
                      data => data.ToString(),
                      data => (ApplicationRole)Enum.Parse(typeof(ApplicationRole), data));

            builder.HasMany<IdentityUser>(u => u.Users)
               .WithMany(r => r.Roles)
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
        }
    }
}
