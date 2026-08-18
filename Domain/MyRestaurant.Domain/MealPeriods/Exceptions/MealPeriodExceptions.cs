using MyRestaurant.Domain.Messages;
using MyRestaurant.Framework.Exceptions;

namespace MyRestaurant.Domain.MealPeriods.Exceptions
{
    internal class MealPeriodExceptions
    {
        public static Error MealPeriodNameRequired = new(RestaurantMessages.MealPeriodExceptions_Title, RestaurantMessages.MealPeriodNameRequired);
        public static Error MealPeriodInvalidTime = new(RestaurantMessages.MealPeriodExceptions_Title, RestaurantMessages.MealPeriodInvalidTime);
        public static Error MealPeriodNameExists = new(RestaurantMessages.MealPeriodExceptions_Title, RestaurantMessages.MealPeriodNameExists);
    }
}
