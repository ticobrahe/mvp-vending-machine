using Application.Command.Product;
using FluentValidation;

namespace Application.Validators.ProductValidator
{
    public class UpdateProductCommandValidator: AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Cost).GreaterThan(0).Must(x => x % 5 == 0).WithMessage("Cost should be multiple of 5");
            RuleFor(x => x.AmountAvailable).GreaterThan(0);
            RuleFor(x => x.ProductName).NotEmpty();
        }
    }
}
