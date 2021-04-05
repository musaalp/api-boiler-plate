using FluentValidation;

namespace Data.Orders.GetOrderDetailsByOrderId
{
    public class GetOrderDetailsByOrderIdDataRequestValidator : AbstractValidator<GetOrderDetailsByOrderIdDataRequest>
    {
        public GetOrderDetailsByOrderIdDataRequestValidator()
        {
            RuleFor(r => r.OrderId)
                .NotEmpty()
                .WithMessage(r => $"{nameof(r.OrderId)}_should_not_be_null_or_empty");
        }
    }
}
