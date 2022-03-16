using System;
using System.Text.Json.Serialization;
using Common.General;
using MediatR;

namespace Application.Command.Product
{
    public class UpdateProductCommand: IRequest<SuccessResponse<UpdateProductCommandResponse>>
    {
        [JsonIgnore]
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string ProductName { get; set; }
        public int AmountAvailable { get; set; }
        public decimal Cost { get; set; }
    }

    public class UpdateProductCommandResponse
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public int AmountAvailable { get; set; }
        public decimal Cost { get; set; }
    }
}
