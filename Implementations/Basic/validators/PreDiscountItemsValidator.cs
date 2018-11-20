using FluentValidation;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class PreDiscountItemsValidator : AbstractValidator<IPreDiscountItemsArgs>
    {
        public PreDiscountItemsValidator()
        {
            RuleFor(x => x.PreDiscountItems)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
