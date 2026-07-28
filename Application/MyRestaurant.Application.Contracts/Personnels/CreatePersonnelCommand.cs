using MediatR;

namespace MyRestaurant.Application.Contracts.Personnels
{
    public class CreatePersonnelCommand : IRequest<PersonnelDTO>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
