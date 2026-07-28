using MediatR;
using MyRestaurant.Application.Query.Contracts.Shared;

namespace MyRestaurant.Application.Query.Contracts.Personnels
{
    public class GetPersonnelQuery : IRequest<PersonnelQueryResult>
    {
        public PaginationParams PaginationParams { get; set; }
    }
}
