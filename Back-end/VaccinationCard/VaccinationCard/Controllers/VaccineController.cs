using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using VaccinationCard.Application.DTOs.Requests;
using VaccinationCard.Application.Vaccines.Commands.CreateVaccine;
using VaccinationCard.Application.Vaccines.Queries.GetAllVaccines;

namespace VaccinationCard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController(IMediator mediator) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> RegisterVaccine(CreateVaccineRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateVaccineCommand(request.Name, request.DoseQuantity);

            var result = await _mediator.Send(command, cancellationToken);

            return result.IsSuccess ? Created() : BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVaccines(CancellationToken cancellationToken)
        {
            var query = new GetAllVaccineQuery();

            var result = await _mediator.Send(query, cancellationToken);

            return StatusCode((int)result.StatusCode, result.IsSuccess ? result.Value : result.Error);
        }
    }
}
