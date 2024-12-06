using HSS.Domain.IdentityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class ReceptionistConfigurations : IEntityTypeConfiguration<Receptionist>
    {
        public void Configure(EntityTypeBuilder<Receptionist> builder)
        {
            builder.HasOne(b => b.Reception)
                .WithMany(p => p.Receptionists)
                .HasForeignKey(a => a.ReceptionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
