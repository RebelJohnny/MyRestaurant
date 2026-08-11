using MyRestaurant.Framework.Exceptions;
using MyRestaurant.Framework.Messages;

namespace MyRestaurant.Framework.Querying.Filters
{
    public class InvalidNumericArrayFilterException() : RichException(FrameworkMessages.InvalidFilterException, FrameworkMessages.InvalidFilterExpectedNumericArray) { }
    public class InvalidNumericFilterException() : RichException(FrameworkMessages.InvalidFilterException, FrameworkMessages.InvalidFilterExpectedNumber) { }
    public class InvalidStringFilterException() : RichException(FrameworkMessages.InvalidFilterException, FrameworkMessages.InvalidFilterExpectedString) { }
    public class InvalidFilterTypeException() : RichException(FrameworkMessages.InvalidFilterException, FrameworkMessages.InvalidFilterType) { }
    public class InvalidDateFilterException() : RichException(FrameworkMessages.InvalidFilterException, FrameworkMessages.InvalidFilterExpectedDate) { }
    public class InvalidDateArrayFilterException() : RichException(FrameworkMessages.InvalidFilterException, FrameworkMessages.InvalidFilterExpectedDateArray) { }
}