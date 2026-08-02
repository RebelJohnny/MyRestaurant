using Microsoft.EntityFrameworkCore;
using MyRestaurant.Application.Query.Contracts.MenuItems;
using MyRestaurant.Domain.MenuItems.Entities;

namespace MyRestaurant.EF.Read.Repositories.MenuItems
{
    internal class MenuItemQueryRepository(RestaurantQueryContext context) : IMenuItemQueryRepository
    {
        private protected DbSet<MenuItem> dbSet = context.MenuItems;
        public async Task<MenuItemFormData?> GetById(long id, CancellationToken cancellationToken)
        {
            return await dbSet.Where(mi => mi.Id == id).Select(mi => new MenuItemFormData
            {
                Id = mi.Id, 
                Name = mi.Name,
                Type = mi.Type,
            }).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<IEnumerable<MenuItemQueryResult>> GetAll(CancellationToken cancellationToken)
        {
            return await dbSet.Select(mi => new MenuItemQueryResult
            {
                Id = mi.Id,
                Name = mi.Name,
                Type = (short)mi.Type,
                CreatedAt = mi.CreatedAt,
            }).ToListAsync(cancellationToken);
        }
    }
}
