using HSS.DataAccess.Configurations;
using HSS.Domain.BaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.DataAccess.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<IdentityUser<int>> IdentityUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityUserConfigurations).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
