using Application.Command.User;
using Common.General;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContext;
using System.Net;

namespace Persistence.CommandHandlers.User
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, SuccessResponse<UpdateUserCommandResponse>>
    {
        private readonly AppDbContext _context;

        public UpdateUserCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SuccessResponse<UpdateUserCommandResponse>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "User not found");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            await _context.SaveChangesAsync(cancellationToken);

            var response = new UpdateUserCommandResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = user.Role,
            };
            return new SuccessResponse<UpdateUserCommandResponse> { Data = response, Message = "User Updated successfully" };
        }
    }
}
