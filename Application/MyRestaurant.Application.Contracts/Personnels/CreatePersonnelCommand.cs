using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.Personnels
{
    public class CreatePersonnelCommand : ICommand<PersonnelDTO>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
