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
    public class UserBuyCommand: IRequest<SuccessResponse<UserBuyCommandResponse>>
    {
        [JsonIgnore]
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
    }

    public class UserBuyCommandResponse
    {
        public decimal AmountSpent { get; set; }
        public decimal Change { get; set; }
        public string ProductName { get; set; }
    }
}
