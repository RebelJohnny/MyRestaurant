using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyRestaurant.Framework.Data.Exceptions;
using MyRestaurant.Framework.Extensions;

namespace MyRestaurant.Framework.Data
{
    public class UnitOfWork(DbContext context) : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            //NormalizeStrings();
            try
            {
                return await context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw DatabaseExceptions.Concurrency(ex);
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException sqlException)
                {
                    throw sqlException.Number switch
                    {
                        2601 or 2627 => DatabaseExceptions.Duplicate(sqlException),
                        547 => DatabaseExceptions.ForeignKeyViolation(sqlException),
                        515 => DatabaseExceptions.RequiredValue(sqlException),
                        2628 => DatabaseExceptions.ValueTooLong(sqlException),
                        _ => DatabaseExceptions.Unknown(sqlException)
                    };
                }

                throw DatabaseExceptions.Unknown(ex);
            }
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
