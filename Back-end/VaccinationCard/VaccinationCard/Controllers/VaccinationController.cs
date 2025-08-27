using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Api.Extensions;
using VaccinationCard.Application.Vaccinations.Commands.CreateVaccination;
using VaccinationCard.Application.Vaccinations.Commands.DeleteVaccination;
using VaccinationCard.Application.Vaccinations.Commands.UpdateVaccination;

namespace VaccinationCard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VaccinationController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> RegisterVaccination(CreateVaccinationCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return ResultExtension.HandleResult(this, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVaccination(UpdateVaccinationCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return ResultExtension.HandleResult(this, result);
        }

        [HttpDelete("{vaccinationId}")]
        public async Task<IActionResult> DeleteVaccinationById([FromRoute(Name = "vaccinationId")] Guid VaccinationId, CancellationToken cancellationToken)
        {
            var command = new DeleteVaccinationCommand(VaccinationId);

            var result = await _mediator.Send(command, cancellationToken);

            return ResultExtension.HandleResult(this, result);
        }
    }
}
