using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.General;
using MediatR;

namespace Application.Command.User
{
    public class LoginCommand: IRequest<SuccessResponse<LoginResponse>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public Guid Id { get; set; }
        public string AccessToken { get; set; }

    }
}
