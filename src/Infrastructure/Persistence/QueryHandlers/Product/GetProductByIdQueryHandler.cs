using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Command.Product;
using Application.Query.Product;
using Common.General;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContext;

namespace Persistence.QueryHandlers.Product
{
    public class GetProductByIdQueryHandler: IRequestHandler<GetProductByIdQuery, SuccessResponse<GetProductByIdQueryResponse>>
    {
        private readonly AppDbContext _context;

        public GetProductByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SuccessResponse<GetProductByIdQueryResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await  _context.Products.Where(x => x.Id == request.ProductId).Select(x => new GetProductByIdQueryResponse
            {
                Id = x.Id,
                AmountAvailable = x.AmountAvailable,
                Cost = x.Cost,
                Name = x.ProductName,
                Seller = new SellerDto
                {
                    Id = x.Seller.Id,
                    UserName = x.Seller.UserName
                }
            }).FirstOrDefaultAsync(cancellationToken);

            if (product == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "Product not found");
            }

            return new SuccessResponse<GetProductByIdQueryResponse> { Data = product, Message = "Product retrieved successfully" };

        }
    }
}
