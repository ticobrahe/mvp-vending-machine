using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Command.User;
using Common.General;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContext;

namespace Persistence.CommandHandlers.User
{
    public class UserDepositCommandHandler: IRequestHandler<UserDepositCommand, SuccessResponse<UserDepositCommandResponse>>

    {
        private readonly AppDbContext _context;

        public UserDepositCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SuccessResponse<UserDepositCommandResponse>> Handle(UserDepositCommand request, CancellationToken cancellationToken)
        {
            var buyer = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            buyer.Deposit += request.Deposit;
            await _context.SaveChangesAsync(cancellationToken);

            var response = new UserDepositCommandResponse
            {
                Deposit = request.Deposit,
                TotalDeposit = buyer.Deposit
            };

            return new SuccessResponse<UserDepositCommandResponse> { Data = response, Message = "Amount deposited successfully" };
        }
    }
}
