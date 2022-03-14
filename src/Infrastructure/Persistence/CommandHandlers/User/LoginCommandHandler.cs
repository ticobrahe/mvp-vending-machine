using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Command.User;
using Common.General;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContext;
using Persistance.Jwt;

namespace Persistence.CommandHandlers.User
{
    public class LoginCommandHandler: IRequestHandler<LoginCommand, SuccessResponse<LoginResponse>>
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(AppDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }
        public async Task<SuccessResponse<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);
            if (user == null)
            {
                throw new RestException(HttpStatusCode.BadRequest, "User not found");
            }

            var isUserValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
            if (!isUserValid)
            {
                throw new RestException(HttpStatusCode.BadRequest, "Wrong userName or password");
            }

            if (user.IsLogin)
            {
                throw new RestException(HttpStatusCode.Conflict, "There is already an active session using your account");
            }

            user.IsLogin = true;
            await _context.SaveChangesAsync();

           var userAccessToken = _jwtService.GenerateAsync(user);

           var response = new LoginResponse
           {
               Id = user.Id,
               AccessToken = userAccessToken.AccessToken
           };
           return new SuccessResponse<LoginResponse> { Data = response, Message = "Login successfully" };
        }
    }
}
