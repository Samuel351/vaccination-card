using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Application.Persons.Commands.CreatePerson;
using VaccinationCard.Application.Persons.DTOs.Requests;
using VaccinationCard.Application.Persons.Queries.GetPersonById;

namespace VaccinationCard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController(IMediator mediator) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> RegisterPerson([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            var command = new CreatePersonCommand(request.Name);

            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode((int) result.StatusCode);
        }

        [HttpGet("/{PersonId}")]
        public async Task<IActionResult> GetPersonById([FromRoute] Guid PersonId, CancellationToken cancellationToken)
        {

            var query = new GetPersonByIdQuery(PersonId);

            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode((int)result.StatusCode, result.IsSuccess ? result.Value : result.Error);
        }
    }
}
