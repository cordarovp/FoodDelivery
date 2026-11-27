using FoodDelivery.Entity;
using FoodDelivery.Entity.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FoodDelivery.Data
{
    public class AuditingInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if(eventData.Context == null)
            {
                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            IEnumerable<EntityEntry<ISoftDeletable>> entries =
                eventData
                .Context
                .ChangeTracker
                .Entries<ISoftDeletable>()
                .Where(e => e.State == EntityState.Deleted);

            IEnumerable<EntityEntry<Order>> orderEntries =
                eventData
                .Context
                .ChangeTracker
                .Entries<Order>()
                .Where(e => e.State == EntityState.Added);

            foreach (var entry in entries)
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
                entry.Entity.DeletedAt = DateTime.UtcNow;

                if(entry.Entity is User user)
                {
                    var suffix = "_del_";
                    var uniqueNumber = Guid.NewGuid().ToString("N");
                    var deleteSuffix = suffix + uniqueNumber;
                    user.Username += deleteSuffix;
                }
            }

            foreach(var entry in orderEntries)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
