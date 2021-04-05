using FluentValidation;

namespace Data.Orders.CheckOrderIsExists
{
    public class CheckOrderIsExistsDataRequestValidator : AbstractValidator<CheckOrderIsExistsDataRequest>
    {
        public CheckOrderIsExistsDataRequestValidator()
        {
            RuleFor(r => r.OrderId)
                .NotEmpty()
                .WithMessage(r => $"{nameof(r.OrderId)}_should_not_be_null_or_empty");
        }
    }
}
