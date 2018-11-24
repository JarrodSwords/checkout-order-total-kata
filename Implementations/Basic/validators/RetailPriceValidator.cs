using FluentValidation;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class RetailPriceValidator : AbstractValidator<IRetailPriceArgs>
    {
        public RetailPriceValidator()
        {
            RuleFor(x => x.RetailPrice)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}
