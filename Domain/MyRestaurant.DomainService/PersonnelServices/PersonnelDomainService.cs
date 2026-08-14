using MyRestaurant.Domain.Personnels;
using MyRestaurant.EF.Read.Repositories.Personnels;

namespace MyRestaurant.DomainService.PersonnelServices
{
    internal class PersonnelDomainService(IPersonnelQueryRepository queryRepository) : IPersonnelDomainService
    {
        public async Task<bool> CheckCodeExistence(long id, string code)
        {
            return await queryRepository.CheckCodeExistence(id, code);
        }
    }
}
