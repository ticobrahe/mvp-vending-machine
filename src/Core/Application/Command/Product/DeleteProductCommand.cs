using System;
using MediatR;

namespace Application.Command.Product
{
    public class DeleteProductCommand: IRequest<Unit>
    {
        public Guid ProductId { get; set; }
    }
}
