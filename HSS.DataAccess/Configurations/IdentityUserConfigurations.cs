using HSS.Domain.BaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.DataAccess.Configurations
{
    internal class IdentityUserConfigurations : IEntityTypeConfiguration<IdentityUser<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUser<int>> builder)
        {
            builder.Property(b => b.Id).IsRequired();
            builder.HasKey(b => b.Id);
        }
    }
}
