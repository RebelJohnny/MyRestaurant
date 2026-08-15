using MyRestaurant.Application.Query.Contracts.Personnels;
using MyRestaurant.Framework.Data;
using MyRestaurant.Framework.Querying;
using MyRestaurant.Framework.Querying.Pagination;

namespace MyRestaurant.EF.Read.Repositories.Personnels
{
    public interface IPersonnelQueryRepository : IQueryRepository
    {
        Task<bool> CheckCodeExistence(long id, string code);
        Task<PagedResult<PersonnelQueryResult>> GetList(QueryParams queryParams, CancellationToken cancellationToken);
        Task<PersonnelFormData?> GetById(long id, CancellationToken cancellationToken);
        Task<List<PersonnelReservedOrderOnMealPeriodQueryResult>> GetReservedOrdersBetweenDates(long personnelId, DateTime startDate, DateTime endDate, long mealPeriodId, CancellationToken cancellationToken);
        Task<List<PersonnelQueryResult>> GetAll(CancellationToken cancellationToken);
    }
}
