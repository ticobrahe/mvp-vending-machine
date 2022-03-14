using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.General;
using MediatR;

namespace Application.Command.Product
{
    public class AddProductCommand: IRequest<SuccessResponse<AddProductCommandResponse>>
    {
        public string Name { get; set; }
        public int AmountAvailable { get; set; } 
        public decimal Cost { get; set; }
        public Guid Seller { get; set; }
    }

    public class AddProductCommandResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AmountAvailable { get; set; }
        public decimal Cost { get; set; }
        public SellerDto Seller { get; set; }
    }

    public class SellerDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}
