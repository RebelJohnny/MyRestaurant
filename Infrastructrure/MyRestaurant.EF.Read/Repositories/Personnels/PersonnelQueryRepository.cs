using Microsoft.EntityFrameworkCore;
using MyRestaurant.Application.Query.Contracts.Personnels;
using MyRestaurant.Application.Query.Contracts.Reports.AllPersonnelDailyReserves;
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
            return await dbSet.Where(p => p.Id == personnelId).SelectMany(p => p.Reserves.Where(pro =>
            pro.Date.Date.CompareTo(startDate) >= 0 &&
            pro.Date.Date.CompareTo(endDate) <= 0)).Select(pro => new PersonnelReservedOrderOnMealPeriodQueryResult
            {
                Date = pro.Date,
                Articles = pro.Meals.Where(proa => proa.MealPeriodId == mealPeriodId).Select(proa => new PersonnelReservedMealOnMealPeriodQueryResult
                {
                    Id = proa.Id,
                    Count = proa.Count
                })
            }).ToListAsync(cancellationToken);
        }
        public async Task<List<PersonnelReservedMealOnMealPeriodQueryResult>> GetReservesOnDayMealPeriod(DateTimeOffset date, long mealPeriodId, CancellationToken cancellationToken)
        {
            return await dbSet
                .SelectMany(p => p.Reserves.Where(pr => pr.Date == date.Date))
                .SelectMany(pr => pr.Meals.Where(prm => prm.PersonnelReserveId == mealPeriodId))
                .Select(prm => new PersonnelReservedMealOnMealPeriodQueryResult
                {
                    Id = prm.Id,
                    Count = prm.Count
                })
                .GroupBy(prm => prm.Id).Select(g => new PersonnelReservedMealOnMealPeriodQueryResult
                {
                    Id = g.Key,
                    Count = g.Sum(prm => prm.Count)
                }).ToListAsync(cancellationToken);
        }
    }
}
