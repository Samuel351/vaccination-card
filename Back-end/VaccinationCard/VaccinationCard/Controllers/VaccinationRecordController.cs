using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Application.VaccinationRecords.Commands;

namespace VaccinationCard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationRecordController(IMediator mediator) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> RegisterVaccinationRecord(CreateVaccinationRecordCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return StatusCode((int)result.StatusCode, result.IsSuccess ? result.Value : result.Error);
        }
       
    }
}
