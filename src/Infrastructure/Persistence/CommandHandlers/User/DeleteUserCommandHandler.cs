using Application.Command.User;
using Common.General;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContext;
using System.Net;

namespace Persistence.CommandHandlers.User
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly AppDbContext _context;

        public DeleteUserCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

            if (user == null)
            {
                throw new RestException(HttpStatusCode.NotFound, "User not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
