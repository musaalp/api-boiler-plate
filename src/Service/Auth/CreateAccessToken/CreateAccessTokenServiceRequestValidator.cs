using FluentValidation;

namespace Service.Auth.CreateAccessToken
{
    public class CreateAccessTokenServiceRequestValidator : AbstractValidator<CreateAccessTokenServiceRequest>
    {
        public CreateAccessTokenServiceRequestValidator()
        {
            RuleFor(r => r.UserId)
                .GreaterThan(0)
                .WithMessage(r => $"{nameof(r.UserId)}_should_be_greater_than_zero");

            RuleFor(r => r.FirstName)
                .NotEmpty()
                .WithMessage(r => $"{nameof(r.FirstName)}_should_not_be_null_or_empty");

            RuleFor(r => r.UserName)
                .NotEmpty()
                .WithMessage(r => $"{nameof(r.UserName)}_should_not_be_null_or_empty")
                .MinimumLength(3)
                .WithMessage(r => $"{nameof(r.UserName)}_should_be_more_than_3_characters")
                .MaximumLength(30)
                .WithMessage(r => $"{nameof(r.UserName)}_should_not_be_more_than_30_characters");

            RuleFor(r => r.LastName)
                .NotEmpty()
                .WithMessage(r => $"{nameof(r.LastName)}_should_not_be_null_or_empty");

            RuleFor(r => r.ExpiryDays)
                .GreaterThan(0)
                .WithMessage(r => $"{nameof(r.ExpiryDays)}_should_be_greater_than_zero")
                .When(r => r.ExpiryDays.HasValue);
        }
    }
}
