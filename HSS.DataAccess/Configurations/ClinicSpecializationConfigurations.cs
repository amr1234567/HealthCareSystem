using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class ClinicSpecializationConfigurations : IEntityTypeConfiguration<ClinicSpecialization>
    {
        public void Configure(EntityTypeBuilder<ClinicSpecialization> builder)
        {
            
        }
    }
}
