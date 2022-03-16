using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Command.Product;
using Common.General;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContext;

namespace Persistence.CommandHandlers.Product
{
    public class AddProductCommandHandler: IRequestHandler<AddProductCommand, SuccessResponse<AddProductCommandResponse>>
    {
        private readonly AppDbContext _context;

        public AddProductCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SuccessResponse<AddProductCommandResponse>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var seller = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.SellerId);

            var product = new Domain.Entities.Product
            {
                AmountAvailable = request.AmountAvailable,
                Cost = request.Cost,
                ProductName = request.ProductName,
                SellerId = request.SellerId
            };

            await _context.AddAsync(product);
            await _context.SaveChangesAsync(cancellationToken);

            var response = new AddProductCommandResponse
            {
                Id = product.Id,
                ProductName = product.ProductName,
                AmountAvailable = product.AmountAvailable,
                Cost = product.Cost,
                Seller = new SellerDto
                {
                    Id = seller.Id,
                    UserName = seller.UserName
                }
            };
            return new SuccessResponse<AddProductCommandResponse> { Data = response, Message = "Product added successfully" };
        }
    }
}
