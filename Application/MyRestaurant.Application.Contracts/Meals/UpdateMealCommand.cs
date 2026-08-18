using MyRestaurant.Domain.Meals.Enums;
using MyRestaurant.Domain.Shared.Abstracts;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.Meals
{
    public sealed class UpdateMealCommand : ICommand<Result>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public MealTypeEnum Type { get; set; }
    }
}
