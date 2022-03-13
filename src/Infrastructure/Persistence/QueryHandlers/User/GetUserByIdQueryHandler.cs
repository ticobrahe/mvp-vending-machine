using Application.Query.User;
using Common.General;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContext;
using System.Net;

namespace Persistence.QueryHandlers.User
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, SuccessResponse<GetUserQueryByIdResponse>>
    {
        private readonly AppDbContext _context;

        public GetUserByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SuccessResponse<GetUserQueryByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.Where(x => x.Id == request.Id).Select(x => new GetUserQueryByIdResponse
            {
                Id = x.Id,
                UserName = x.UserName,
                Role = x.Role,
            }).FirstOrDefaultAsync();

            if (user == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "User not found");
            }

            return new SuccessResponse<GetUserQueryByIdResponse> { Data = user, Message = "User retrieved successfully" };
        }
    }
}
