using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Application.Contracts.MenuItems;
using MyRestaurant.Application.Query.Contracts.MenuItems;
using MyRestaurant.Endpoint.WebApi.Models;

namespace MyRestaurant.Endpoint.WebApi.Controllers.MenuItems.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController(IMediator mediator) : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ResponseModel<MenuItemDTO>>> Post(CreateMenuItemCommand command, CancellationToken cancellationToken)
        {
            var data = await mediator.Send(command, cancellationToken);
            return Respond(ResponseModel<MenuItemDTO>.Created(data));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel>> Put(long id, UpdateMenuItemCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            await mediator.Send(command, cancellationToken);
            return Respond(ResponseModel.Ok());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel>> Delete(long id, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteMenuItemCommand { Id = id }, cancellationToken);
            return Respond(ResponseModel.NoContent());
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<MenuItemQueryResult>>>> Get(CancellationToken cancellationToken)
        {
            var data = await mediator.Send(new GetMenuItemsQuery(), cancellationToken);
            return Respond(ResponseModel<IEnumerable<MenuItemQueryResult>>.Ok(data));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<MenuItemFormData>>> Get(long id, CancellationToken cancellationToken)
        {
            var data = await mediator.Send(new GetMenuItemFormDataQuery { Id = id }, cancellationToken);
            return Respond(ResponseModel<MenuItemFormData>.Ok(data));
        }
    }
}
