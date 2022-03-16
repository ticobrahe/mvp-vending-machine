using Application.Command.Product;
using Common.General;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContext;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Persistence.CommandHandlers.Product
{
    public class DeleteProductCommandHandler: IRequestHandler<DeleteProductCommand>
    {
        private readonly AppDbContext _context;

        public DeleteProductCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId);

            if (product == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Product not found");
            }

            if (product.SellerId != request.UserId)
            {
                throw new RestException(HttpStatusCode.Forbidden, "Forbidden");
            }

            _context.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
