using Microsoft.EntityFrameworkCore;
using MyRestaurant.Domain.Personnels;
using MyRestaurant.Domain.Personnels.Entities;
using MyRestaurant.Framework.Data;

namespace MyRestaurant.EF.Repositories
{
    public class PersonnelRepository(RestaurantContext context) : Repository<Personnel>(context), IPersonnelRepository
    {
        private protected DbSet<Personnel> dbSet = context.Set<Personnel>();
        public async Task<Personnel?> GetById(long id, CancellationToken cancellationToken)
        {
            return await dbSet.Include(p => p.Reserves).ThenInclude(pro => pro.Meals).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
    }
}
