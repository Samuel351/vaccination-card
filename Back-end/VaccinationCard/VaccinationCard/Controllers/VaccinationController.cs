using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Application.Vaccinations.Commands.CreateVaccination;
using VaccinationCard.Application.Vaccinations.Commands.DeleteVaccination;
using VaccinationCard.Application.Vaccinations.Commands.UpdateVaccination;
using VaccinationCard.Application.Vaccines.Commands.DeleteVaccine;

namespace VaccinationCard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> RegisterVaccination(CreateVaccinationRecordCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode((int)result.StatusCode, !result.IsSuccess ? result.Error : null);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVaccination(UpdateVaccinationCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode((int)result.StatusCode);
        }

        [HttpDelete("{vaccinationId}")]
        public async Task<IActionResult> DeleteVaccinationById([FromRoute(Name = "vaccinationId")] Guid VaccinationId, CancellationToken cancellationToken)
        {
            var command = new DeleteVaccinationCommand(VaccinationId);

            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode((int)result.StatusCode);
        }
    }
}
