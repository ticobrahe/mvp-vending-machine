using Common.General;
using MediatR;

namespace Application.Query.User
{
    public class GetUsersQuery: IRequest<SuccessResponse<List<GetUsersQueryResponse>>>
    {
    }

    public class GetUsersQueryResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
