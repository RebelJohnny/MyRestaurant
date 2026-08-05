using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Application.Contracts.Meals;
using MyRestaurant.Application.Query.Contracts.Meals;
using MyRestaurant.Endpoint.WebApi.Models;

namespace MyRestaurant.Endpoint.WebApi.Controllers.Meals.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController(IMediator mediator) : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ResponseModel<MealDTO>>> Post(CreateMealCommand command, CancellationToken cancellationToken)
        {
            var data = await mediator.Send(command, cancellationToken);
            return Respond(ResponseModel<MealDTO>.Created(data));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel>> Put(long id, UpdateMealCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            await mediator.Send(command, cancellationToken);
            return Respond(ResponseModel.Ok());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel>> Delete(long id, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteMealCommand { Id = id }, cancellationToken);
            return Respond(ResponseModel.NoContent());
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<MealQueryResult>>>> Get(CancellationToken cancellationToken)
        {
            var data = await mediator.Send(new GetMealsQuery(), cancellationToken);
            return Respond(ResponseModel<IEnumerable<MealQueryResult>>.Ok(data));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<MealFormData>>> Get(long id, CancellationToken cancellationToken)
        {
            var data = await mediator.Send(new GetMealFormDataQuery { Id = id }, cancellationToken);
            return Respond(ResponseModel<MealFormData>.Ok(data));
        }
    }
}
