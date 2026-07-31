using MyRestaurant.Application.Contracts.MealPeriods;
using MyRestaurant.Domain.MealPeriods.Args;
using MyRestaurant.Domain.MealPeriods.Entities;

namespace MyRestaurant.Application.MealPeriods
{
    internal static class MealPeriodMapper
    {
        internal static MealPeriodArgs Map(CreateMealPeriodCommand command)
        {
            return new MealPeriodArgs
            {
                Name = command.Name,
                Time = command.Time
            };
        }
        internal static MealPeriodDTO Map(MealPeriod mealPeriod)
        {
            return new MealPeriodDTO(mealPeriod.Id, mealPeriod.Name, mealPeriod.Time);
        }
    }
}
