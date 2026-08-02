using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.Personnels
{
    public sealed class DeletePersonnelCommand : ICommand
    {
        public long Id { get; set; }
    }
}
