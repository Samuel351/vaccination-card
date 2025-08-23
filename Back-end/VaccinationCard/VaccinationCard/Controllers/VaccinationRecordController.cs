using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Application.VaccinationRecords.Commands;
using VaccinationCard.Application.VaccinationRecords.DTOs.Requests;

namespace VaccinationCard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationRecordController(IMediator mediator) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> RegisterVaccinationRecord(CreateVaccinationRequest request)
        {
            var command = new CreateVaccinationRecordCommand(request.PersonId, request.VaccineId, request.DoseNumber, request.VaccinationDate);

            var result = await _mediator.Send(command);

            return StatusCode((int)result.StatusCode, result.IsSuccess ? result.Value : result.Error);
        }

        // Registar
    }
}
