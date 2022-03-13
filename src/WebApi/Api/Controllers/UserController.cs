using Application.Command.User;
using Application.Query.User;
using Common.General;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Register for a new user.
        /// </summary>
        /// <param name="command"></param>
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponse<AddUserCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SuccessResponse<>), (int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Get a user by Id
        /// </summary>
        /// <param name="id"></param>
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponse<GetUserQueryByIdResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SuccessResponse<>), (int)HttpStatusCode.NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByid(Guid id)
        {
            var query = new GetUserByIdQuery
            {
                Id = id
            };
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="id"></param>
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponse<UpdateUserCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SuccessResponse<>), (int)HttpStatusCode.NotFound)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id"></param>
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponse<>), (int)HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var command = new DeleteUserCommand
            {
                Id = id
            };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
