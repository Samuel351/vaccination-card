using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Application.Vaccines.Commands.CreateVaccine;
using VaccinationCard.Application.Vaccines.DTOs.Requests;

namespace VaccinationCard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController(IMediator mediator) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> RegisterVaccine(CreateVaccineRequest request)
        {
            var command = new CreateVaccineCommand(request.Name, request.DoseQuantity);

            var result = await _mediator.Send(command);

            return result.IsSuccess ? Created() : BadRequest();
        }
    }
}
