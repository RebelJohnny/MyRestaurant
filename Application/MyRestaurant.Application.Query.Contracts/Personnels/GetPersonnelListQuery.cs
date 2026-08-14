using MyRestaurant.Framework.Mediator;
using MyRestaurant.Framework.Querying;

namespace MyRestaurant.Application.Query.Contracts.Personnels
{
    public sealed class GetPersonnelListQuery : IQuery<IEnumerable<PersonnelQueryResult>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
