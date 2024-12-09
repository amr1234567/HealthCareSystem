using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class RadiologyTestTypeConfigurations : IEntityTypeConfiguration<RadiologyTestType>
    {
        public void Configure(EntityTypeBuilder<RadiologyTestType> builder)
        {
            builder.HasKey(x => x.Id);

        }
    }
}
