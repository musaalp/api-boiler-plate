using FluentValidation;

namespace Data.Orders.GetProductsByUnitTypes
{
    public class GetProductsByUnitTypesDataRequestValidator : AbstractValidator<GetProductsByUnitTypesDataRequest>
    {
        public GetProductsByUnitTypesDataRequestValidator()
        {
            RuleFor(r => r.UnitTypes)
                .NotEmpty()
                .WithMessage(r => $"{nameof(r.UnitTypes)}_should_not_be_null_or_empty");
        }
    }
}
