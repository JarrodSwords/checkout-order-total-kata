using FluentValidation;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class GroupSalePriceValidator : AbstractValidator<IGroupSalePriceArgs>
    {
        public GroupSalePriceValidator()
        {
            RuleFor(x => x.GroupSalePrice)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
