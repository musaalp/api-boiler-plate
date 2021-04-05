using FluentValidation;

namespace Data.Users.GetUser
{
    public class GetUserDataRequestValidator : AbstractValidator<GetUserDataRequest>
    {
        public GetUserDataRequestValidator()
        {
            RuleFor(r => r.UserName)
                .NotEmpty()
                .WithMessage(r => $"{nameof(r.UserName)}_should_not_be_null_or_empty")
                .MinimumLength(3)
                .WithMessage(r => $"{nameof(r.UserName)}_should_be_more_than_3_characters")
                .MaximumLength(30)
                .WithMessage(r => $"{nameof(r.UserName)}_should_not_be_more_than_30_characters");

            RuleFor(r => r.PasswordHash)
                .NotEmpty()
                .WithMessage(r => $"{nameof(r.PasswordHash)}_should_not_be_null_or_empty");
        }
    }
}
