using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Contracts.Personnels
{
    public sealed class GetAllPersonnelsQuery : IQuery<IEnumerable<PersonnelQueryResult>>
    {
    }
}
