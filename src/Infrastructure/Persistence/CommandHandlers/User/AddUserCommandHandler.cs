using Application.Command.User;
using Common.General;
using MediatR;
using Persistance.DbContext;
using System.Net;

namespace Persistence.CommandHandlers.User
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, SuccessResponse<AddUserCommandResponse>>
    {
        private readonly AppDbContext _context;
        public AddUserCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SuccessResponse<AddUserCommandResponse>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var userName = request.UserName.ToLower();
            var userExists = _context.Users.Any(x => x.UserName.ToLower() == userName);
            if (userExists)
            {
                throw new RestException(HttpStatusCode.BadRequest, "User already exists");
            }

            var user = new Domain.Entities.User
            {
                UserName = request.UserName,
                Role = request.Role,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var response = new AddUserCommandResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Role = user.Role,
            };
            return new SuccessResponse<AddUserCommandResponse> { Data = response, Message = "User created successfully" };
        }
    }
}
