using Application.Command.User;
using Common.General;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Login a user.
        /// </summary>
        /// <param name="command"></param>
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponse<LoginResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SuccessResponse<>), (int)HttpStatusCode.BadRequest)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
