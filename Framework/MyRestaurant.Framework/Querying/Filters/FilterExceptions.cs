using MyRestaurant.Framework.Exceptions;
using MyRestaurant.Framework.Messages;

namespace MyRestaurant.Framework.Querying.Filters
{
    internal class FilterExceptions
    {
        public static Error InvalidNumericArrayFilterException = new(){
            Title = FrameworkMessages.InvalidFilterException, 
            Message = FrameworkMessages.InvalidFilterExpectedNumericArray 
        };
        public static Error InvalidNumericFilterException = new() { Title = FrameworkMessages.InvalidFilterException, Message = FrameworkMessages.InvalidFilterExpectedNumber };
        public static Error InvalidStringFilterException = new() { Title = FrameworkMessages.InvalidFilterException, Message = FrameworkMessages.InvalidFilterExpectedString };
        public static Error InvalidFilterTypeException = new() { Title = FrameworkMessages.InvalidFilterException, Message = FrameworkMessages.InvalidFilterType };
        public static Error InvalidDateFilterException = new() { Title = FrameworkMessages.InvalidFilterException, Message = FrameworkMessages.InvalidFilterExpectedDate };
        public static Error InvalidDateArrayFilterException = new() { Title = FrameworkMessages.InvalidFilterException, Message = FrameworkMessages.InvalidFilterExpectedDateArray };
    }
}