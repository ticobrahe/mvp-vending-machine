using Common.General;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Command.User
{
    public class UpdateUserCommand: IRequest<SuccessResponse<UpdateUserCommandResponse>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserCommandResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
