using HSS.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class AppointmentConfigurations : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(b => b.Hospital)
                .WithMany()
                .HasForeignKey(a => a.HospitalId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            
        }
    }
}
