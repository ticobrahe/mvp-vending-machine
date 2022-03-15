using Application.Command.User;
using Common.General;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContext;

namespace Persistence.CommandHandlers.User
{
    internal class LogoutCommandHandler : IRequestHandler<LogoutCommand, SuccessResponse<string>>
    {
        private readonly AppDbContext _context;

        public LogoutCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SuccessResponse<string>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            user.IsLogin = false;
            await _context.SaveChangesAsync();
            return new SuccessResponse<string> { Message = "Account Session terminated successfully" };
        }
    }
}
