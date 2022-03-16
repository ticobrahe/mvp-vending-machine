using Application.Command.User;
using FluentValidation;

namespace Application.Validators.UserValidator
{
    public class UserBuyCommandValidator: AbstractValidator<UserBuyCommand>
    {
        public UserBuyCommandValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0);
        }
    }
}
