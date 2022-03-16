using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Common.General;
using MediatR;

namespace Application.Command.Product
{
    public class AddProductCommand: IRequest<SuccessResponse<AddProductCommandResponse>>
    {
        [JsonIgnore]
        public Guid SellerId { get; set; }
        public string ProductName { get; set; }
        public int AmountAvailable { get; set; } 
        public decimal Cost { get; set; }
    }

    public class AddProductCommandResponse
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
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
