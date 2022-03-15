using Application.Command.User;
using Common.General;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContext;

namespace Persistence.CommandHandlers.User
{
    public class ResetDepositCommandHandler : IRequestHandler<ResetDepositCommand, SuccessResponse<string>>
    {
        private readonly AppDbContext _context;

        public ResetDepositCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SuccessResponse<string>> Handle(ResetDepositCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId);
            user.Deposit = 0;
            await _context.SaveChangesAsync();
            return new SuccessResponse<string> { Message = "Deposit reset successfully" };
        }
    }
}
