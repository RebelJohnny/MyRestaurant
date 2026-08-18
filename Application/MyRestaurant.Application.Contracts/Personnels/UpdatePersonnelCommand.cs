using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.Personnels
{
    public sealed class UpdatePersonnelCommand : ICommand<Result>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
