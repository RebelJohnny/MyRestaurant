using Microsoft.EntityFrameworkCore;
using MyRestaurant.Application.Query.Contracts.Personnels;
using MyRestaurant.Domain.Personnels.Entities;

namespace MyRestaurant.EF.Read.Repositories.Personnels
{
    internal class PersonnelQueryRepository(RestaurantQueryContext context) : IPersonnelQueryRepository
    {
        private protected DbSet<Personnel> dbSet = context.Personnels;
        public async Task<PersonnelFormData?> GetById(long id, CancellationToken cancellationToken)
        {
            return await dbSet.Where(p => p.Id == id).Select(p => new PersonnelFormData
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name
            }).FirstOrDefaultAsync(cancellationToken);
        }
        public async Task<IEnumerable<PersonnelQueryResult>> GetAll(CancellationToken cancellationToken)
        {
            return await dbSet.Select(p => new PersonnelQueryResult
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                CreatedAt = p.CreatedAt
            }).ToListAsync(cancellationToken);
        }
        public async Task<List<PersonnelReservedOrderOnMealPeriodQueryResult>> GetReservedOrdersBetweenDates(long personnelId, DateTime startDate, DateTime endDate, long mealPeriodId, CancellationToken cancellationToken)
        {
            return await dbSet.Where(p => p.Id == personnelId).SelectMany(p => p.ReservedOrders.Where(pro =>
            pro.Date.Date.CompareTo(startDate) >= 0 &&
            pro.Date.Date.CompareTo(endDate) <= 0)).Select(pro => new PersonnelReservedOrderOnMealPeriodQueryResult
            {
                Date = pro.Date,
                Articles = pro.Articles.Where(proa => proa.MealPeriodId == mealPeriodId).Select(proa => new PersonnelReservedOrderArticleOnMealPeriodQueryResult
                {
                    Id = proa.Id,
                    MealId = proa.MealId,
                    Count = proa.Count
                })
            }).ToListAsync(cancellationToken);
        }
    }
}
