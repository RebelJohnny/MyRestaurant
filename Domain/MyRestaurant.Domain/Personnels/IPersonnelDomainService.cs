namespace MyRestaurant.Domain.Personnels
{
    public interface IPersonnelDomainService
    {
        Task<bool> CheckCodeExistence(long id, string code);
    }
}
