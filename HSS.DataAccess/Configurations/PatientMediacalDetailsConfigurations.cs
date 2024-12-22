using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class PatientMediacalDetailsConfigurations : IEntityTypeConfiguration<PatientMediacalDetails>
    {
        public void Configure(EntityTypeBuilder<PatientMediacalDetails> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
