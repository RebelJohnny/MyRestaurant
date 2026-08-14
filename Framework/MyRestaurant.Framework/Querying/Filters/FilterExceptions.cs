using MyRestaurant.Framework.Exceptions;
using MyRestaurant.Framework.Messages;

namespace MyRestaurant.Framework.Querying.Filters
{
    internal class FilterExceptions
    {
        public static Error InvalidNumericArrayFilterException = new(FrameworkMessages.InvalidFilterException, FrameworkMessages.InvalidFilterExpectedNumericArray);
        public static Error InvalidNumericFilterException = new(FrameworkMessages.InvalidFilterException, FrameworkMessages.InvalidFilterExpectedNumber);
        public static Error InvalidStringFilterException = new(FrameworkMessages.InvalidFilterException, FrameworkMessages.InvalidFilterExpectedString);
        public static Error InvalidFilterTypeException = new(FrameworkMessages.InvalidFilterException, FrameworkMessages.InvalidFilterType);
        public static Error InvalidDateFilterException = new(FrameworkMessages.InvalidFilterException, FrameworkMessages.InvalidFilterExpectedDate);
        public static Error InvalidDateArrayFilterException = new(FrameworkMessages.InvalidFilterException, FrameworkMessages.InvalidFilterExpectedDateArray);
    }
}