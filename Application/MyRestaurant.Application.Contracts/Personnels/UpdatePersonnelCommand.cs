using MediatR;

namespace MyRestaurant.Application.Contracts.Personnels
{
    public class UpdatePersonnelCommand : IRequest
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
