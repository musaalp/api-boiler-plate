using FluentValidation;
using Service.Orders.Models;

namespace Service.Orders.AddOrder
{
    public class AddOrderServiceRequestValidator : AbstractValidator<AddOrderServiceRequest>
    {
        public AddOrderServiceRequestValidator()
        {
            RuleFor(r => r.OrderId)
                .NotEmpty()
                .WithMessage(r => $"{nameof(r.OrderId)}_should_not_be_null_or_empty");

            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage(r => $"{nameof(r.Email)}_should_not_be_null_or_empty")
                .EmailAddress()
                .WithMessage(r => $"{nameof(r.Email)}_should_be_in_correct_format");

            RuleFor(r => r.Products)
                .NotEmpty()
                .WithMessage(r => $"{nameof(r.Products)}_should_not_be_null_or_empty");

            RuleForEach(r => r.Products)
                .SetValidator(new ProductModelValidator());
        }
    }
}
