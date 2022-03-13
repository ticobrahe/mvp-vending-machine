using MediatR;

namespace Application.Command.User
{
    public class DeleteUserCommand: IRequest
    {
        public Guid Id { get; set; }
    }
}
