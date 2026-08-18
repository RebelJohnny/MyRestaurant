using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Endpoint.WebApi.Controllers;
using MyRestaurant.Framework.Exceptions;

namespace MyRestaurant.Endpoint.WebApi.Models
{
    public static class ErrorExtensions
    {
        public static ActionResult ToActionResult(this Error error, ApiControllerBase controller)
        {
            return controller.Problem(title: error.Title, detail: error.Message, statusCode: error.StatusCode);
        }
    }
}
