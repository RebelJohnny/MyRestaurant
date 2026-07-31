using Microsoft.EntityFrameworkCore;
using MyRestaurant.Framework.Extensions;

namespace MyRestaurant.Framework.Data
{
    public class UnitOfWork(DbContext context) : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync()
        {
            NormalizeStrings();
            return await context.SaveChangesAsync();
        }
        private void NormalizeStrings()
        {
            var entries = context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
            foreach (var entry in entries)
            {
                foreach (var prop in entry.Properties.Where(p => p.Metadata.ClrType == typeof(string)))
                {
                    var current = prop.CurrentValue as string;
                    if (!string.IsNullOrEmpty(current))
                    {
                        prop.CurrentValue = current.ToPersian();
                    }
                }
            }
        }
    }
}
