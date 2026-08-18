using MyRestaurant.Domain.Meals.Enums;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.Meals
{
    public sealed class CreateMealCommand : ICommand<Result<MealDTO>>
    {
        public string Name { get; set; }
        public MealTypeEnum Type { get; set; }
    }
}
