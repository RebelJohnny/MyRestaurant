using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Application.Contracts.Personnels;
using MyRestaurant.Application.Query.Contracts.Personnels;
using MyRestaurant.Endpoint.WebApi.Models;
using MyRestaurant.Framework.Querying;

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
        [HttpPost("GetList")]
        public async Task<ActionResult<ResponseModel<IEnumerable<PersonnelQueryResult>>>> Get(QueryParams queryParams, CancellationToken cancellationToken)
        {
            var data = await mediator.Send(new GetPersonnelListQuery { QueryParams = queryParams }, cancellationToken);
            return Respond(ResponseModel<IEnumerable<PersonnelQueryResult>>.Ok(data));
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<PersonnelFormData>>> Get(long id, CancellationToken cancellationToken)
        {
            var data = await mediator.Send(new GetPersonnelFormDataQuery { Id = id }, cancellationToken);
            return Respond(ResponseModel<PersonnelFormData>.Ok(data));
        }
        [HttpGet("Reserves/{id}")]
        public async Task<ActionResult<ResponseModel<IEnumerable<PersonnelReserveQueryResult>>>> Get(long id, [FromQuery] GetPersonnelReservedOrdersQuery query, CancellationToken cancellationToken)
        {
            query.PersonnelId = id;
            var data = await mediator.Send(query, cancellationToken);
            return Respond(ResponseModel<IEnumerable<PersonnelReserveQueryResult>>.Ok(data));
        }
        [HttpPut("Reserves/{id}")]
        public async Task<ActionResult<ResponseModel>> Put(long id, ReserveForPersonnelCommand command, CancellationToken cancellationToken)
        {
            command.PersonnelId = id;
            await mediator.Send(command, cancellationToken);
            return Respond(ResponseModel.Ok());
        }
    }
}
