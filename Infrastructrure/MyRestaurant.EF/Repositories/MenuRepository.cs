using Microsoft.EntityFrameworkCore;
using MyRestaurant.Domain.Menus;
using MyRestaurant.Domain.Menus.Entities;
using MyRestaurant.Framework.Data;

namespace MyRestaurant.EF.Repositories
{
    public class MenuRepository(RestaurantContext context) : Repository<Menu>(context), IMenuRepository
    {
        private protected DbSet<Menu> dbSet = context.Set<Menu>();
        public async Task<Menu?> GetByDate(DateTimeOffset date, CancellationToken cancellationToken)
        {
            return await dbSet.Include(m => m.Meals).FirstOrDefaultAsync(x => x.Date == date, cancellationToken);
        }
    }
}
