using FluentValidation;

namespace Service.Orders.Models
{
    public class ProductModelValidator : AbstractValidator<ProductModel>
    {
        public ProductModelValidator()
        {
            RuleFor(r => r.UnitType)
               .NotEmpty()
               .WithMessage(r => $"{nameof(r.UnitType)}_should_not_be_null_or_empty");

            RuleFor(r => r.Quantity)
                .GreaterThan(0)
                .WithMessage(r => $"{nameof(r.Quantity)}_should_be_greater_than_zero");
        }
    }
}
