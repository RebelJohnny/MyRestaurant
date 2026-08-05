using MyRestaurant.Application.Contracts.Meals;
using MyRestaurant.Domain.Meals.Args;
using MyRestaurant.Domain.Meals.Entities;

namespace MyRestaurant.Application.Meals
{
    internal static class MealMapper
    {
        internal static MealDTO Map(Meal meal)
        {
            return new MealDTO(meal.Id, meal.Name, (short)meal.Type);
        }
        internal static MealArgs Map(CreateMealCommand command)
        {
            return new MealArgs
            {
                Name = command.Name,
                Type = command.Type
            };
        }
        internal static MealArgs Map(UpdateMealCommand command)
        {
            return new MealArgs
            {
                Name = command.Name,
                Type = command.Type
            };
        }
    }
}
