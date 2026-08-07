using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Application.Contracts.Menus;
using MyRestaurant.Application.Query.Contracts.Menus;
using MyRestaurant.Endpoint.WebApi.Models;

namespace MyRestaurant.Endpoint.WebApi.Controllers.Menus.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController(IMediator mediator) : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<MenuQueryResult>>>> Get([FromQuery] GetMenuQuery query, CancellationToken cancellationToken)
        {
            var data = await mediator.Send(query, cancellationToken);
            return Respond(ResponseModel<IEnumerable<MenuQueryResult>>.Ok(data));
        }
        [HttpPut]
        public async Task<ActionResult<ResponseModel>> Put(UpdateMenuOnDayCommand command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return Respond(ResponseModel.Ok());
        }
    }
}
