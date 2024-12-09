using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSS.DataAccess.Configurations
{
    public class AdministrationConfigurations : IEntityTypeConfiguration<Administration>
    {
        public void Configure(EntityTypeBuilder<Administration> builder)
        {
            builder.HasKey(x => x.Id);

        }
    }
}
