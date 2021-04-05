using FluentValidation;
using Sdk.Core.Entities;

namespace Data.Orders.AddOrder
{
    public class AddOrderDataRequestValidator : AbstractValidator<AddOrderDataRequest>
    {
        public AddOrderDataRequestValidator()
        {
            RuleFor(r => r.Order.Email)
                .NotEmpty()
                .WithMessage(r => $"{nameof(r.Order.Email)}_should_not_be_null_or_empty")
                .EmailAddress()
                .WithMessage(r => $"{nameof(r.Order.Email)}_should_be_in_correct_format");

            RuleFor(r => r.Order.OrderId)
                .NotEmpty()
                .WithMessage(r => $"{nameof(r.Order.OrderId)}_should_not_be_null_or_empty");

            RuleFor(r => r.OrderDetails)
                .NotEmpty()
                .WithMessage(r => $"{nameof(r.OrderDetails)}_should_not_be_null_or_empty");

            RuleForEach(r => r.OrderDetails)
                .SetValidator(new OrderDetailEntityValidator());
        }
    }

    public class OrderDetailEntityValidator : AbstractValidator<OrderDetailEntity>
    {
        public OrderDetailEntityValidator()
        {
            RuleFor(r => r.Quantity)
                .GreaterThan(0)
                .WithMessage(r => $"{nameof(r.Quantity)}_should_be_greater_than_zero");

            RuleFor(r => r.ProductId)
                .GreaterThan(0)
                .WithMessage(r => $"{nameof(r.ProductId)}_should_be_greater_than_zero");
        }
    }
}
