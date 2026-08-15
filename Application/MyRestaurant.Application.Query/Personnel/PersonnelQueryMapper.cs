using MyRestaurant.Application.Query.Contracts.Meals;
using MyRestaurant.Application.Query.Contracts.Personnels;

namespace MyRestaurant.Application.Query.Personnel
{
    internal static class PersonnelQueryMapper
    {
        internal static List<PersonnelReserveQueryResult> Map(List<PersonnelReservedOrderOnMealPeriodQueryResult> reservedOrders, List<MealFormData> meals)
        {
            return [.. reservedOrders.Select(ro => new PersonnelReserveQueryResult
            {
                Date = ro.Date,
                DayOfWeek = (int)ro.Date.DayOfWeek,
                Meals = ro.Articles.Select(roa =>
                {
                    var meal = meals.First(m => m.Id == roa.Id);
                    return new PersonnelReservedMeal
                    {
                        Id = roa.Id,
                        Name = meal.Name,
                        Type = meal.Type,
                        Count = roa.Count
                    };
                })
            })];
        }
    }
}
