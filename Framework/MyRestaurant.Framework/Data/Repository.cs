using Microsoft.EntityFrameworkCore;

namespace MyRestaurant.Framework.Data
{
    public class Repository<T>(DbContext context) : IRepository<T> where T : class
    {
        private protected DbSet<T> dbSet = context.Set<T>();

        public virtual async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual async Task<T?> GetById(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }
    }
}
