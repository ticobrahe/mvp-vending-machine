using Application.Command.Product;
using Common.General;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContext;
using System.Net;

namespace Persistence.CommandHandlers.Product
{
    public class UpdateProductCommandHandler: IRequestHandler<UpdateProductCommand, SuccessResponse<UpdateProductCommandResponse>>
    {
        private readonly AppDbContext _context;

        public UpdateProductCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SuccessResponse<UpdateProductCommandResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId);

            if (product == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Product not found");
            }

            product.ProductName = request.ProductName;
            product.AmountAvailable = request.AmountAvailable;
            product.Cost = request.Cost;

            await _context.SaveChangesAsync(cancellationToken);
            var response = new UpdateProductCommandResponse
            {
                Id = product.Id,
                Cost = product.Cost,
                ProductName = product.ProductName,
                AmountAvailable = product.AmountAvailable
            };

            return new SuccessResponse<UpdateProductCommandResponse> { Data = response, Message = "Product updated successfully" };
        }
    }
}
