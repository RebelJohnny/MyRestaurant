using MyRestaurant.Domain.Menus.Entities;
using MyRestaurant.Framework.Data;

namespace MyRestaurant.Domain.Menus
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<Menu?> GetByDate(DateTimeOffset date, CancellationToken cancellationToken);
    }
}
