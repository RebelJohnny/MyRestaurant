using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Contracts.Personnels
{
    public sealed class GetPersonnelQuery : IQuery<IEnumerable<PersonnelQueryResult>>
    {
        //public PaginationParams PaginationParams { get; set; }
    }
}
