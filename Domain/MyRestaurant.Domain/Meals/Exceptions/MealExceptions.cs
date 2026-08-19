using MyRestaurant.Domain.Messages;
using MyRestaurant.Framework.Exceptions;

namespace MyRestaurant.Domain.Meals.Exceptions
{
    internal class MealExceptions
    {
        public static Error MealNameRequired = new() { Title = RestaurantMessages.MealExceptions_Title, Message = RestaurantMessages.MealNameRequired };
        public static Error MealTypeRequired = new() { Title = RestaurantMessages.MealExceptions_Title, Message = RestaurantMessages.MealTypeRequired };
        public static Error MealNameExists = new() { Title = RestaurantMessages.MealExceptions_Title, Message = RestaurantMessages.MealNameExists };
    }
}
