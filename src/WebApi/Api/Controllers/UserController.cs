using Application.Command.User;
using Application.Query.User;
using Common.General;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Common.Helper;
using Application;

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
        /// Register a new user.
        /// </summary>
        /// <param name="command"></param>
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponse<AddUserCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.BadRequest)]
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
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponse<GetUserQueryByIdResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
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
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.NotFound)]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            command.UserId = User.GetUserId();
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id"></param>
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse), (int)HttpStatusCode.NotFound)]
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

        /// <summary>
        /// Buyer deposit
        /// </summary>
        /// <param name="command"></param>
        //[Authorize(Roles = Role.Buyer)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponse<UserDepositCommandResponse>), (int)HttpStatusCode.OK)]
        [HttpPost("deposit")]
        public async Task<IActionResult> UserDeposit([FromBody] UserDepositCommand command)
        {
            command.UserId = User.GetUserId();
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Buy a product
        /// </summary>
        /// <param name="command"></param>
        [Authorize(Roles = Role.Buyer)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponse<UserBuyCommandResponse>), (int)HttpStatusCode.OK)]
        [HttpPost("buy")]
        public async Task<IActionResult> ProductPurchase([FromBody] UserBuyCommand command)
        {
            command.UserId = User.GetUserId();
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Reset deposit
        /// </summary>
        [Authorize(Roles = Role.Buyer)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponse<string>), (int)HttpStatusCode.OK)]
        [HttpPost("reset")]
        public async Task<IActionResult> ResetDeposit()
        {
            var command = new ResetDepositCommand
            {
                UserId = User.GetUserId(),
            };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Get users
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponse<List<GetUsersQueryResponse>>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var query = new GetUsersQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
