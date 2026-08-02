using MyRestaurant.Application.Query.Contracts.Personnels;
using MyRestaurant.Framework.Data;

namespace MyRestaurant.EF.Read.Repositories.Personnels
{
    public interface IPersonnelQueryRepository : IQueryRepository
    {
        Task<IEnumerable<PersonnelQueryResult>> GetAll(CancellationToken cancellationToken);
        Task<PersonnelFormData?> GetById(long id, CancellationToken cancellationToken);
    }
}
