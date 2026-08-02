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
    }
}
