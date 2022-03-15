using Application.Query.User;
using Common.General;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContext;

namespace Persistence.QueryHandlers.User
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, SuccessResponse<List<GetUsersQueryResponse>>>
    {
        private readonly AppDbContext _context;

        public GetUsersQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SuccessResponse<List<GetUsersQueryResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.Select(x => new GetUsersQueryResponse
            {
                Id = x.Id,
                UserName = x.UserName,
                Role = x.Role
            }).ToListAsync();

           return new SuccessResponse<List<GetUsersQueryResponse>> {Data = users, Message = "Deposit reset successfully" };

        }
    }
}




