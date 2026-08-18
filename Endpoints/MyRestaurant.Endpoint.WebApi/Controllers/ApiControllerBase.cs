using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Endpoint.WebApi.Models;
using MyRestaurant.Framework.Exceptions;

namespace MyRestaurant.Endpoint.WebApi.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ActionResult<ResponseModel<T>> Respond<T>(ResponseModel<T> response) => response.ToActionResult();
        protected ActionResult<ResponseModel> Respond(ResponseModel response) => response.ToActionResult();
        protected ActionResult Respond(Error error) => error.ToActionResult(this);
    }
}
