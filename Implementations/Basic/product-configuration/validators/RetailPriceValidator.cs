using FluentValidation;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class RetailPriceValidator : AbstractValidator<IUpsertEachesProductArgs>
    {
        public RetailPriceValidator()
        {
            RuleFor(x => x.RetailPrice)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}
