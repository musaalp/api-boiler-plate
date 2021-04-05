using FluentValidation;

namespace Service.Orders.GetOrderByOrderId
{
    public class GetOrderByOrderIdServiceRequestValidator : AbstractValidator<GetOrderByOrderIdServiceRequest>
    {
        public GetOrderByOrderIdServiceRequestValidator()
        {
            RuleFor(r => r.OrderId)
                .NotEmpty()
                .WithMessage(r => $"{nameof(r.OrderId)}_should_not_be_null_or_empty");
        }
    }
}
