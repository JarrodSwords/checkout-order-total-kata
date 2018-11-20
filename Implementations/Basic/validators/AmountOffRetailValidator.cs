using FluentValidation;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class AmountOffRetailValidator : AbstractValidator<IAmountOffRetailArgs>
    {
        public AmountOffRetailValidator()
        {
            RuleFor(x => x.AmountOffRetail)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
