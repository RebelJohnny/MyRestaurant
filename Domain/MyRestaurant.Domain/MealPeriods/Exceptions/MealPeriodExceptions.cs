using MyRestaurant.Domain.Messages;
using MyRestaurant.Framework.Exceptions;

namespace MyRestaurant.Domain.MealPeriods.Exceptions
{
    internal class MealPeriodExceptions
    {
        public static Error MealPeriodNameRequired = new() { Title = RestaurantMessages.MealPeriodExceptions_Title, Message = RestaurantMessages.MealPeriodNameRequired };
        public static Error MealPeriodInvalidTime = new() { Title = RestaurantMessages.MealPeriodExceptions_Title, Message = RestaurantMessages.MealPeriodInvalidTime };
        public static Error MealPeriodNameExists = new() { Title = RestaurantMessages.MealPeriodExceptions_Title, Message = RestaurantMessages.MealPeriodNameExists };
    }
}
