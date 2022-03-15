using Application.Command.User;
using Common.General;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContext;
using System.Net;

namespace Persistence.CommandHandlers.User
{
    public class UserBuyCommandHandler: IRequestHandler<UserBuyCommand, SuccessResponse<UserBuyCommandResponse>>
    {
        private readonly AppDbContext _context;

        public UserBuyCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SuccessResponse<UserBuyCommandResponse>> Handle(UserBuyCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == request.ProductId);
            ValidateProduct(product, request.Amount);

            var buyer = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            var totalPrice = product.Cost * request.Amount;

            if (totalPrice > buyer.Deposit)
            {
                throw new RestException(HttpStatusCode.BadRequest, "Deposit not sufficient to buy the product");
            }

            product.AmountAvailable -= request.Amount;
            buyer.Deposit -= totalPrice;
            await _context.SaveChangesAsync(cancellationToken);

            var response = new UserBuyCommandResponse
            {
                AmountSpent = totalPrice,
                Change = buyer.Deposit,
                ProductName = product.Name
            };

            return new SuccessResponse<UserBuyCommandResponse> { Data = response, Message = "Product purchased successfully" };
        }

        private void ValidateProduct(Domain.Entities.Product product, int amount)
        {
            if (product == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Product not found");
            }

            if (product.AmountAvailable == 0)
            {
                throw new RestException(HttpStatusCode.BadRequest, "Product is not available. Kindly check back");
            }

            if (amount > product.AmountAvailable)
            {
                throw new RestException(HttpStatusCode.BadRequest, $"Amount is more than product amount available {product.AmountAvailable}");
            }
        }
    }
}
