using Application.Features.Partners.Commands;
using FluentValidation;

namespace Application.Validators.Features.Partners.Commands
{
    public class PartnerRequestValidator : AbstractValidator<AddEditPartnerCommand>
    {
        public PartnerRequestValidator()
        {
            RuleFor(p => p.PartnerRequest.Name)
                .Must(f => !string.IsNullOrWhiteSpace(f)).WithMessage("Name is required")
                .MaximumLength(30).WithMessage("Name must not exceed 30 characters.")
                .Matches(@"^[a-zA-Z]+$").WithMessage("Name must contain alphabets only.");
        }
    }
}
