using MyRestaurant.Application.Query.Contracts.Meals;
using MyRestaurant.Application.Query.Contracts.Personnels;

namespace MyRestaurant.Application.Query.Personnel
{
    internal static class PersonnelQueryMapper
    {
        internal static List<PersonnelReservedOrderQueryResult> Map(List<PersonnelReservedOrderOnMealPeriodQueryResult> reservedOrders, List<MealFormData> meals)
        {
            return [.. reservedOrders.Select(ro => new PersonnelReservedOrderQueryResult
            {
                Date = ro.Date,
                DayOfWeek = ro.Date.DayOfWeek,
                Meals = ro.Articles.Select(roa =>
                {
                    var meal = meals.First(m => m.Id == roa.MealId);
                    return new PersonnelReservedOrderMeal
                    {
                        Id = roa.MealId,
                        Name = meal.Name,
                        Type = meal.Type,
                        Count = roa.Count
                    };
                })
            })];
        }
    }
}
