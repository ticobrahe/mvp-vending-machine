using Common.General;
using MediatR;

namespace Application.Command.User
{
    public class AddUserCommand: IRequest<SuccessResponse<AddUserCommandResponse>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public  string Role { get; set; }
    }

    public class AddUserCommandResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
