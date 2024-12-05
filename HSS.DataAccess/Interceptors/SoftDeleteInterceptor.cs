using HSS.Domain.BaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.DataAccess.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            var context = eventData.Context;
            if (context == null) return base.SavingChanges(eventData, result);

            HandleSoftDelete(context.ChangeTracker);

            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;
            if (context != null)
            {
                HandleSoftDelete(context.ChangeTracker);
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void HandleSoftDelete(ChangeTracker changeTracker)
        {
            var entries = changeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted && e.Entity is BaseClass<int>)
                .ToList();

            foreach (var entry in entries)
            {
                // Cancel the deletion and mark as "soft deleted"
                entry.State = EntityState.Modified;
                entry.CurrentValues["IsDeleted"] = true;
            }
        }
    }

}
