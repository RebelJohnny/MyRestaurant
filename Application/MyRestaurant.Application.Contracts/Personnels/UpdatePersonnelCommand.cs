using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.Personnels
{
    public class UpdatePersonnelCommand : ICommand
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
