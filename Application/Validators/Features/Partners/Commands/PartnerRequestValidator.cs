using FluentValidation;
using Shared.Requests.Partners;

namespace Application.Validators.Features.Partners.Commands
{
    public class PartnerRequestValidator : AbstractValidator<PartnerRequest>
    {
        public PartnerRequestValidator()
        {
            RuleFor(p => p.Name)
                .Must(f => !string.IsNullOrWhiteSpace(f)).WithMessage("Name is required")
                .MaximumLength(30).WithMessage("Name must not exceed 30 characters.")
                .Matches(@"^[a-zA-Z]+$").WithMessage("Name must contain alphabets only.");
        }
    }
}
