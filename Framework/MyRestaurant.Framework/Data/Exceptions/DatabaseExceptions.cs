using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyRestaurant.Framework.Exceptions;

namespace MyRestaurant.Framework.Data.Exceptions
{
    internal class DatabaseExceptions
    {
        //    public static Error FromDbException(Exception exception)
        //    {
        //        return new Error
        //        {
        //            Title = ExceptionMessages.Database_Title,
        //            Message = exception.InnerException.Message,
        //            StatusCode = StatusCodes.Status500InternalServerError
        //        };
        //    }
        public static Error Concurrency(DbUpdateConcurrencyException ex)
        {
            return new Error
            {
                Title = ExceptionMessages.DatabaseConcurrency_Title,
                Message = ExceptionMessages.DatabaseConcurrency_Description,
                StatusCode = StatusCodes.Status409Conflict,
                InnerExceptionMessage = ex.Message
            };
        }
        public static Error Duplicate(SqlException ex)
        {
            return new Error
            {
                Title = ExceptionMessages.DatabaseDuplicate_Title,
                Message = ExceptionMessages.DatabaseDuplicate_Description,
                StatusCode = StatusCodes.Status409Conflict,
                InnerExceptionMessage = ex.Message
            };
        }
        public static Error ForeignKeyViolation(SqlException ex)
        {
            return new Error
            {
                Title = ExceptionMessages.DatabaseForeignKeyViolation_Title,
                Message = ExceptionMessages.DatabaseForeignKeyViolation_Description,
                StatusCode = StatusCodes.Status409Conflict,
                InnerExceptionMessage = ex.Message
            };
        }
        public static Error RequiredValue(SqlException ex)
        {
            return new Error
            {
                Title = ExceptionMessages.DatabaseDataRequired_Title,
                Message = ExceptionMessages.DatabaseDataRequired_Description,
                InnerExceptionMessage = ex.Message
            };
        }
        public static Error ValueTooLong(SqlException ex)
        {
            return new Error
            {
                Title = ExceptionMessages.DatabaseDataTooLong_Title,
                Message = ExceptionMessages.DatabaseDataTooLong_Description,
                InnerExceptionMessage = ex.Message
            };
        }
        public static Error Unknown(Exception ex)
        {
            return new Error
            {
                Title = ExceptionMessages.Database_Title,
                Message = ExceptionMessages.Database_Description,
                StatusCode = StatusCodes.Status500InternalServerError,
                InnerExceptionMessage = ex.Message
            };
        }
    }
}
