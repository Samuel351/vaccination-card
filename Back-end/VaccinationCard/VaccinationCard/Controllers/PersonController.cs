using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Application.DTOs.Requests;
using VaccinationCard.Application.Persons.Commands.CreatePerson;
using VaccinationCard.Application.Persons.Commands.DeletePerson;
using VaccinationCard.Application.Persons.Queries.GetPersonById;
using VaccinationCard.Application.Persons.Queries.GetPersonVaccinationCard;

namespace VaccinationCard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController(IMediator mediator) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> RegisterPerson([FromBody] CreatePersonCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode((int) result.StatusCode);
        }

        [HttpGet("{personId}")]
        public async Task<IActionResult> GetPersonById([FromRoute(Name = "personId")] Guid PersonId, CancellationToken cancellationToken)
        {

            var query = new GetPersonByIdQuery(PersonId);

            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode((int)result.StatusCode, result.IsSuccess ? result.Value : result.Error);
        }

        [HttpDelete("{personId}")]
        public async Task<IActionResult> DeletePersonById([FromRoute(Name = "personId")] Guid PersonId, CancellationToken cancellationToken)
        {
            var command = new DeletePersonCommand(PersonId);

            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode((int)result.StatusCode, result.IsSuccess ? result.Value : result.Error);
        }

        [HttpGet("{personId}/vaccination-card")]
        public async Task<IActionResult> GetVaccinationCard([FromRoute(Name = "personId")] Guid PersonId, CancellationToken cancellationToken)
        {
            var query = new GetPersonVaccinationCardQuery(PersonId);

            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode((int)result.StatusCode, result.IsSuccess ? result.Value : result.Error);
        }
        
    }
}
