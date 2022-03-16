using System;
using Application.Command.Product;
using Common.General;
using MediatR;

namespace Application.Query.Product
{
    public class GetProductByIdQuery: IRequest<SuccessResponse<GetProductByIdQueryResponse>>
    {
        public Guid ProductId { get; set; }
    }

    public class GetProductByIdQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AmountAvailable { get; set; }
        public decimal Cost { get; set; }
        public SellerDto Seller { get; set; }
    }
}
