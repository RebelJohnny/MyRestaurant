using MyRestaurant.Domain.Messages;
using MyRestaurant.Framework.Exceptions;

namespace MyRestaurant.Domain.Personnels.Exceptions
{
    internal class PersonnelExceptions
    {
        public static Error PersonnelCodeRequired = new(RestaurantMessages.PersonnelExceptions_Title, RestaurantMessages.PersonnelCodeRequired);
        public static Error PersonnelNameRequired = new(RestaurantMessages.PersonnelExceptions_Title, RestaurantMessages.PersonnelNameRequired);
        public static Error PersonnelCodeExists = new(RestaurantMessages.PersonnelExceptions_Title, RestaurantMessages.PersonnelCodeExists);

        public static Error ReserveDateInThePast = new(RestaurantMessages.ReserveExceptions_Title, RestaurantMessages.ReserveDateInThePast);
        public static Error MealAlreadyReceived = new(RestaurantMessages.ReserveExceptions_Title, RestaurantMessages.MealAlreadyReceived);
        public static Error MealsNotInMenuForDayPeriod = new(RestaurantMessages.ReserveExceptions_Title, RestaurantMessages.MealsNotInMenuForDayPeriod);
    }
}
