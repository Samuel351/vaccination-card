using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaccinationCard.Api.Extensions;
using VaccinationCard.Application.Users.Commands.CreateUser;

namespace VaccinationCard.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(IMediator mediator) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return ResultExtension.HandleResult(this, result);
        }
    }
}
