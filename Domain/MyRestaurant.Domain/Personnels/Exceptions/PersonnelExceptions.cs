using MyRestaurant.Domain.Messages;
using MyRestaurant.Framework.Exceptions;

namespace MyRestaurant.Domain.Personnels.Exceptions
{
    internal class PersonnelExceptions
    {
        public static Error PersonnelCodeExists = new(RestaurantMessages.PersonnelExceptions_Title, RestaurantMessages.PersonnelCodeExists_ِDescription);
    }
}
