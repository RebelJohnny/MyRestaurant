using MyRestaurant.Domain.Messages;
using MyRestaurant.Framework.Exceptions;

namespace MyRestaurant.Domain.Personnels.Exceptions
{
    internal class PersonnelExceptions
    {
        public static Error PersonnelCodeRequired = new() {
            Title = RestaurantMessages.PersonnelExceptions_Title, 
            Message = RestaurantMessages.PersonnelCodeRequired 
        };
        public static Error PersonnelNameRequired = new() { Title = RestaurantMessages.PersonnelExceptions_Title, Message = RestaurantMessages.PersonnelNameRequired };
        public static Error PersonnelCodeExists = new() { Title = RestaurantMessages.PersonnelExceptions_Title, Message = RestaurantMessages.PersonnelCodeExists };

        public static Error ReserveDateInThePast = new() { Title = RestaurantMessages.ReserveExceptions_Title, Message = RestaurantMessages.ReserveDateInThePast };
        public static Error MealAlreadyReceived = new() { Title = RestaurantMessages.ReserveExceptions_Title, Message = RestaurantMessages.MealAlreadyReceived };
        public static Error MealsNotInMenuForDayPeriod = new() { Title = RestaurantMessages.ReserveExceptions_Title, Message = RestaurantMessages.MealsNotInMenuForDayPeriod };
    }
}
