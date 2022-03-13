using Application.Command.User;
using Domain.Enums;
using FluentValidation;

namespace Application.Validators.UserValidator
{
    public class AddUserCommandValidator: AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Role).IsEnumName(typeof(ERole)).WithMessage("Role not found");
            RuleFor(x => x.Password).Matches(@"(?-i)(?=^.{8,}$)((?!.*\s)(?=.*[A-Z])(?=.*[a-z]))((?=(.*\d){1,})|(?=(.*\W){1,}))^.*$")
                .WithMessage(@"Password must be atleast 8 characters, Atleast 1 upper case letters (A – Z), Atleast 1 lower case letters (a – z), Atleast 1 number (0 – 9) or non-alphanumeric symbol (e.g. @ '$%£! ')");
        }
    }
}
