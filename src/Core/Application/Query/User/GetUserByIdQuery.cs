using Common.General;
using MediatR;

namespace Application.Query.User
{
    public class GetUserByIdQuery: IRequest<SuccessResponse<GetUserQueryByIdResponse>>
    {
        public Guid Id { get; set; }
    }

    public class GetUserQueryByIdResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
