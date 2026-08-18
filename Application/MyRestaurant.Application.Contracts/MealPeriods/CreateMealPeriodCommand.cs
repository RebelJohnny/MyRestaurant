using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.MealPeriods
{
    public sealed class CreateMealPeriodCommand : ICommand<Result<MealPeriodDTO>>
    {
        public string Name { get; set; }
        public int Time { get; set; }
    }
}
