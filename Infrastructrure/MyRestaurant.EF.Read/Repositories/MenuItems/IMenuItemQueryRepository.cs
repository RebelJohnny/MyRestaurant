using MyRestaurant.Application.Query.Contracts.MenuItems;
using MyRestaurant.Framework.Data;

namespace MyRestaurant.EF.Read.Repositories.MenuItems
{
    public interface IMenuItemQueryRepository : IQueryRepository
    {
        Task<IEnumerable<MenuItemQueryResult>> GetAll(CancellationToken cancellationToken);
        Task<MenuItemFormData?> GetById(long id, CancellationToken cancellationToken);
    }
}
