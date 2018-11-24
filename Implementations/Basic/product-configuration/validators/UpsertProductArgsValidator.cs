using FluentValidation;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public abstract class UpsertProductArgsValidator : AbstractValidator<UpsertProductArgs>
    {
        public UpsertProductArgsValidator(
            MassValidator massValidator,
            RetailPriceValidator retailPriceValidator,
            SellByTypeValidator sellByTypeValidator
        )
        {
            Include(retailPriceValidator);
            Include(sellByTypeValidator);
            // When(
            //     x => x.SellByType == "mass",
            //     () => Include(massValidator)
            // );
        }
    }
}
