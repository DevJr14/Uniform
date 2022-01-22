using Application.Features.Promotions.Discounts.Commands;
using FluentValidation;

namespace Application.Validators.Features.Promotions.Commands
{
    public class AddEditDiscountCommandValidator : AbstractValidator<AddEditDiscountCommand>
    {
        public AddEditDiscountCommandValidator()
        {
            RuleFor(d => d.DiscountRequest.Name)
                .NotEmpty().WithMessage("Name cannot be empty");
        }
    }
}
