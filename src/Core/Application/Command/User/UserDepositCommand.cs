using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Common.General;
using MediatR;

namespace Application.Command.User
{
    public class UserDepositCommand: IRequest<SuccessResponse<UserDepositCommandResponse>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public decimal Deposit { get; set; }
    }

    public class UserDepositCommandResponse
    {
        public decimal Deposit { get; set; }
        public decimal TotalDeposit { get; set; }
    }
}
