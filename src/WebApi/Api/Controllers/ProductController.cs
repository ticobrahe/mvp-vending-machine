using Application.Command.Product;
using Application.Query.Product;
using Common.General;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/v1/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add a new product.
        /// </summary>
        /// <param name="command"></param>
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponse<AddProductCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SuccessResponse<>), (int)HttpStatusCode.BadRequest)]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddProductCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Get a product by Id
        /// </summary>
        /// <param name="id"></param>
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponse<GetProductByIdQueryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SuccessResponse<>), (int)HttpStatusCode.NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var query = new GetProductByIdQuery
            {
                Id = id
            };
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponse<UpdateProductCommandResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(SuccessResponse<>), (int)HttpStatusCode.NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductCommand command)
        {
            command.Id = id;
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id"></param>
        [Produces("application/json")]
        [ProducesResponseType(typeof(SuccessResponse<>), (int)HttpStatusCode.NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var command = new DeleteProductCommand
            {
                Id = id
            };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
