using FluentValidation;

namespace Data.Users.GetUserPermissions
{
    public class GetUserPermissionsDataRequestValidator : AbstractValidator<GetUserPermissionsDataRequest>
    {
        public GetUserPermissionsDataRequestValidator()
        {
            RuleFor(r => r.UserId)
                .GreaterThan(0)
                .WithMessage(r => $"{nameof(r.UserId)}_should_be_greater_than_zero");
        }
    }
}
