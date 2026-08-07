using MyRestaurant.Domain.Personnels.Entities;
using MyRestaurant.Framework.Data;

namespace MyRestaurant.Domain.Personnels
{
    public interface IPersonnelRepository : IRepository<Personnel>
    {
        Task<Personnel?> GetById(long id, CancellationToken cancellationToken);
    }
}
