using Application.Command.User;
using FluentValidation;

namespace Application.Validators.UserValidator
{
    public class UserDepositCommandValidator: AbstractValidator<UserDepositCommand>
    {
        private readonly List<decimal> _amountToDeposit = new List<decimal>{5,10,20,50,100};
        public UserDepositCommandValidator()
        {
            RuleFor(x => x.Deposit).GreaterThan(0).Must(x => _amountToDeposit.Contains(x))
                .WithMessage("Deposit can only be 5, 10, 20, 50 and 100");
        }
    }
}
