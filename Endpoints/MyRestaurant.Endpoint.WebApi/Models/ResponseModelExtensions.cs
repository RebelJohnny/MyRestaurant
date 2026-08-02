using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MyRestaurant.Endpoint.WebApi.Models
{
    public static class ResponseModelExtensions
    {
        public static ActionResult<ResponseModel<T>> ToActionResult<T>(
            this ResponseModel<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = (int)response.StatusCode
            };
        }

        public static ActionResult ToActionResult(
            this ResponseModel response)
        {
            if (response.StatusCode == HttpStatusCode.NoContent)
                return new NoContentResult();

            return new ObjectResult(response)
            {
                StatusCode = (int)response.StatusCode
            };
        }
    }
}
