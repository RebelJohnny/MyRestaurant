using MyRestaurant.Application.Query.Contracts.Shared;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Query.Contracts.Personnels
{
    public class GetPersonnelQuery : IQuery<PersonnelQueryResult>
    {
        public PaginationParams PaginationParams { get; set; }
    }
}
