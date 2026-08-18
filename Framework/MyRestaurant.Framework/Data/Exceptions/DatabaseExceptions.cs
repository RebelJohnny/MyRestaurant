using Microsoft.AspNetCore.Http;
using MyRestaurant.Framework.Exceptions;

namespace MyRestaurant.Framework.Data.Exceptions
{
    internal class DatabaseExceptions
    {
        public static Error FromDbException(Exception exception)
        {
            return new Error(ExceptionMessages.Database_Title, exception.InnerException.Message, StatusCodes.Status500InternalServerError);
        }
        public static readonly Error Concurrency = new(ExceptionMessages.DatabaseConcurrency_Title, ExceptionMessages.DatabaseConcurrency_Description, StatusCodes.Status409Conflict);
        public static readonly Error Duplicate = new(ExceptionMessages.DatabaseDuplicate_Title, ExceptionMessages.DatabaseDuplicate_Description, StatusCodes.Status409Conflict);
        public static readonly Error ForeignKeyViolation = new(ExceptionMessages.DatabaseForeignKeyViolation_Title, ExceptionMessages.DatabaseForeignKeyViolation_Description, StatusCodes.Status409Conflict);
        public static readonly Error RequiredValue = new(ExceptionMessages.DatabaseDataRequired_Title, ExceptionMessages.DatabaseDataRequired_Description);
        public static readonly Error ValueTooLong = new(ExceptionMessages.DatabaseDataTooLong_Title, ExceptionMessages.DatabaseDataTooLong_Description);
        public static readonly Error Unknown = new(ExceptionMessages.Database_Title, ExceptionMessages.Database_Description, StatusCodes.Status500InternalServerError);
    }
}
