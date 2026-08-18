using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.Personnels
{
    public sealed class CreatePersonnelCommand : ICommand<Result<PersonnelDTO>>
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
