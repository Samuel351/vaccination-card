using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Api.Extensions;
using VaccinationCard.Application.Persons.Commands.CreatePerson;
using VaccinationCard.Application.Persons.Commands.DeletePerson;
using VaccinationCard.Application.Persons.Commands.UpdatePerson;
using VaccinationCard.Application.Persons.Queries.GetAllPersonPaginated;
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

            return ResultExtension.HandleResult(this, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return ResultExtension.HandleResult(this, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersonsPaginated(CancellationToken cancellationToken, [FromQuery] int PageNumber = 1, [FromQuery] int PageSize = 10, string? Query = null)
        {
            var query = new GetAllPersonPaginatedQuery(PageNumber, PageSize, Query);

            var result = await _mediator.Send(query, cancellationToken);

            return ResultExtension.HandleResult(this, result);
        }

        [HttpGet("{personId}")]
        public async Task<IActionResult> GetPersonById([FromRoute(Name = "personId")] Guid PersonId, CancellationToken cancellationToken)
        {

            var query = new GetPersonByIdQuery(PersonId);

            var result = await _mediator.Send(query, cancellationToken);

            return ResultExtension.HandleResult(this, result);
        }

        [HttpDelete("{personId}")]
        public async Task<IActionResult> DeletePersonById([FromRoute(Name = "personId")] Guid PersonId, CancellationToken cancellationToken)
        {
            var command = new DeletePersonCommand(PersonId);

            var result = await _mediator.Send(command, cancellationToken);

            return ResultExtension.HandleResult(this, result);
        }

        [HttpGet("{personId}/vaccination-card")]
        public async Task<IActionResult> GetVaccinationCard([FromRoute(Name = "personId")] Guid PersonId, CancellationToken cancellationToken)
        {
            var query = new GetPersonVaccinationCardQuery(PersonId);

            var result = await _mediator.Send(query, cancellationToken);

            return ResultExtension.HandleResult(this, result);
        }
        
    }
}
