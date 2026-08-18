using MyRestaurant.Domain.Messages;
using MyRestaurant.Framework.Exceptions;

namespace MyRestaurant.Domain.Menus.Exceptions
{
    internal class MenuExceptions
    {
        public static Error MenuDateInThePast = new(RestaurantMessages.MenuExceptions_Title, RestaurantMessages.MenuDateInThePast);
    }
}
