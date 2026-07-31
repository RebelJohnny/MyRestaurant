using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.Personnels
{
    public class DeletePersonnelCommand : ICommand
    {
        public long Id { get; set; }
    }
}
