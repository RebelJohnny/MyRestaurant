using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Application.Contracts.Personnels;
using MyRestaurant.Application.Query.Contracts.Personnels;
using MyRestaurant.Endpoint.WebApi.Models;

namespace MyRestaurant.Endpoint.WebApi.Controllers.Personnels.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelController(IMediator mediator) : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ResponseModel<PersonnelDTO>>> Post(CreatePersonnelCommand command, CancellationToken cancellationToken)
        {
            var data = await mediator.Send(command, cancellationToken);
            return Respond(ResponseModel<PersonnelDTO>.Created(data));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseModel>> Put(long id, UpdatePersonnelCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            await mediator.Send(command, cancellationToken);
            return Respond(ResponseModel.Ok());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseModel>> Delete(long id, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeletePersonnelCommand { Id = id }, cancellationToken);
            return Respond(ResponseModel.NoContent());
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<PersonnelQueryResult>>>> Get(CancellationToken cancellationToken)
        {
            var data = await mediator.Send(new GetPersonnelQuery(), cancellationToken);
            return Respond(ResponseModel<IEnumerable<PersonnelQueryResult>>.Ok(data));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<PersonnelFormData>>> Get(long id, CancellationToken cancellationToken)
        {
            var data = await mediator.Send(new GetPersonnelFormDataQuery { Id = id }, cancellationToken);
            return Respond(ResponseModel<PersonnelFormData>.Ok(data));
        }
        [HttpGet("ReservedOrders/{id}")]
        public async Task<ActionResult<ResponseModel<IEnumerable<PersonnelReservedOrderQueryResult>>>> Get(long id, [FromQuery]GetPersonnelReservedOrdersQuery query, CancellationToken cancellationToken)
        {
            query.PersonnelId = id;
            var data = await mediator.Send(query, cancellationToken);
            return Respond(ResponseModel<IEnumerable<PersonnelReservedOrderQueryResult>>.Ok(data));
        }
        [HttpPut("ReservedOrders/{id}")]
        public async Task<ActionResult<ResponseModel>> Put(long id, ReserveOrderForPersonnelCommand command, CancellationToken cancellationToken)
        {
            command.PersonnelId = id;
            await mediator.Send(command, cancellationToken);
            return Respond(ResponseModel.Ok());
        }
    }
}
