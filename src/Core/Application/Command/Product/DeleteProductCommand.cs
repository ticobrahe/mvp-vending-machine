using System;
using MediatR;

namespace Application.Command.Product
{
    public class DeleteProductCommand: IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
