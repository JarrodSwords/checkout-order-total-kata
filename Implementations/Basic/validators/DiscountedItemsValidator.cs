using FluentValidation;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class DiscountedItemsValidator : AbstractValidator<IDiscountedItemsArgs>
    {
        public DiscountedItemsValidator()
        {
            RuleFor(x => x.DiscountedItems)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
