using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Application.Vaccines.Commands.CreateVaccine;
using VaccinationCard.Application.Vaccines.Commands.DeleteVaccine;
using VaccinationCard.Application.Vaccines.Commands.UpdateVaccine;
using VaccinationCard.Application.Vaccines.Queries.GetAllVaccines;
using VaccinationCard.Application.Vaccines.Queries.GetVaccineById;

namespace VaccinationCard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController(IMediator mediator) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> RegisterVaccine(CreateVaccineCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode((int)result.StatusCode);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVaccine(UpdateVaccineCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode((int)result.StatusCode);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVaccines(CancellationToken cancellationToken)
        {
            var query = new GetAllVaccineQuery();

            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode((int)result.StatusCode, result.IsSuccess ? result.Value : result.Error);
        }

        [HttpGet("{vaccineId}")]
        public async Task<IActionResult> GetVaccineById([FromRoute(Name = "vaccineId")] Guid VaccineId, CancellationToken cancellationToken)
        {
            var query = new GetVaccineByIdQuery(VaccineId);

            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode((int)result.StatusCode, result.IsSuccess ? result.Value : result.Error);
        }

        [HttpDelete("{vaccineId}")]
        public async Task<IActionResult> DeleteVaccineById([FromRoute(Name = "vaccineId")] Guid VaccineId,CancellationToken cancellationToken)
        {
            var query = new DeleteVaccineCommand(VaccineId);

            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode((int)result.StatusCode);
        }

    }
}
