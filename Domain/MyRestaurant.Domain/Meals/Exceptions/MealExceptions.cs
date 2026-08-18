using MyRestaurant.Domain.Messages;
using MyRestaurant.Framework.Exceptions;

namespace MyRestaurant.Domain.Meals.Exceptions
{
    internal class MealExceptions
    {
        public static Error MealNameRequired = new(RestaurantMessages.MealExceptions_Title, RestaurantMessages.MealNameRequired);
        public static Error MealTypeRequired = new(RestaurantMessages.MealExceptions_Title, RestaurantMessages.MealTypeRequired);
        public static Error MealNameExists = new(RestaurantMessages.MealExceptions_Title, RestaurantMessages.MealNameExists);
    }
}
