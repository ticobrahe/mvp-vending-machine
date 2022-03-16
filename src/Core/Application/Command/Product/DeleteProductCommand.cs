using System;
using System.Text.Json.Serialization;
using MediatR;

namespace Application.Command.Product
{
    public class DeleteProductCommand: IRequest<Unit>
    {
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
