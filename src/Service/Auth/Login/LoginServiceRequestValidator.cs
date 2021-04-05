using FluentValidation;

namespace Service.Auth.Login
{
    public class LoginServiceRequestValidator : AbstractValidator<LoginServiceRequest>
    {
        public LoginServiceRequestValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty()
                .WithMessage(r => $"{nameof(r.UserName)}_should_not_be_null_or_empty")
                .MinimumLength(3)
                .WithMessage(r => $"{nameof(r.UserName)}_should_be_more_than_3_characters")
                .MaximumLength(30)
                .WithMessage(r => $"{nameof(r.UserName)}_should_not_be_more_than_30_characters");

            RuleFor(r => r.Password)
                .NotEmpty()
                .WithMessage(r => $"{nameof(r.Password)}_should_not_be_null_or_empty")
                .MinimumLength(3)
                .WithMessage(r => $"{nameof(r.Password)}_should_be_more_than_3_characters")
                .MaximumLength(20)
                .WithMessage(r => $"{nameof(r.Password)}_should_not_be_more_than_20_characters");
        }
    }
}
