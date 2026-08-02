using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Application.Contracts.MealPeriods;
using MyRestaurant.Application.Query.Contracts.MealPeriods;
using MyRestaurant.Endpoint.WebApi.Models;

namespace MyRestaurant.Endpoint.WebApi.Controllers.MealPeriods.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPeriodController(IMediator mediator) : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ResponseModel<MealPeriodDTO>>> Post(CreateMealPeriodCommand command, CancellationToken cancellationToken)
        {
            var data = await mediator.Send(command, cancellationToken);
            return Respond(ResponseModel<MealPeriodDTO>.Created(data));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel>> Put(long id, UpdateMealPeriodCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            await mediator.Send(command, cancellationToken);
            return Respond(ResponseModel.Ok());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel>> Delete(long id, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteMealPeriodCommand { Id = id }, cancellationToken);
            return Respond(ResponseModel.NoContent());
        }
        [HttpPut("Activate/{id}")]
        public async Task<ActionResult<ResponseModel>> Activate(long id, CancellationToken cancellationToken)
        {
            await mediator.Send(new ActivateMealPeriodCommand { Id = id }, cancellationToken);
            return Respond(ResponseModel.Ok());
        }
        [HttpPut("Deactivate/{id}")]
        public async Task<ActionResult<ResponseModel>> Deactivate(long id, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeactivateMealPeriodCommand { Id = id }, cancellationToken);
            return Respond(ResponseModel.Ok());
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<MealPeriodQueryResult>>>> Get(CancellationToken cancellationToken)
        {
            var data = await mediator.Send(new GetAllMealPeriodsQuery(), cancellationToken);
            return Respond(ResponseModel<IEnumerable<MealPeriodQueryResult>>.Ok(data));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<MealPeriodQueryResult>>> Get(long id, CancellationToken cancellationToken)
        {
            var data = await mediator.Send(new GetMealPeriodFormDataQuery { Id = id }, cancellationToken);
            return Respond(ResponseModel<MealPeriodQueryResult>.Ok(data)); 
        }
    }
}
