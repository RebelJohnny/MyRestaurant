using MyRestaurant.Domain.Meals.Enums;
using MyRestaurant.Framework.Mediator;

namespace MyRestaurant.Application.Contracts.Meals
{
    public sealed class UpdateMealCommand : ICommand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public MealTypeEnum Type { get; set; }
    }
}
