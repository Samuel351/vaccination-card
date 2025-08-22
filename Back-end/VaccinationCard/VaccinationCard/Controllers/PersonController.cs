using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Application.Persons.Commands.CreatePerson;
using VaccinationCard.Application.Persons.DTOs;

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

            await _mediator.Send(command, cancellationToken);

            return Ok();
        }
    }
}
