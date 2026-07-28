using MediatR;

namespace MyRestaurant.Application.Contracts.Personnels
{
    public class DeletePersonnelCommand : IRequest
    {
        public long Id { get; set; }
    }
}
