using Common.General;
using MediatR;
using System.Text.Json.Serialization;

namespace Application.Command.User
{
    public class LogoutCommand: IRequest<SuccessResponse<string>>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
    }
}
