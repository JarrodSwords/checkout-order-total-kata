using AutoMapper;
using FluentValidation;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public abstract class UpsertProductArgsValidator : AbstractValidator<UpsertProductArgs>
    {
        public UpsertProductArgsValidator(
            SellByTypeValidator sellByTypeValidator,
            RetailPriceValidator retailPriceValidator,
            IUpsertMassProductArgsValidator iUpsertMassProductArgsValidator
        )
        {
            Include(sellByTypeValidator);
            When(x => x.SellByType == "eaches", () => Include(retailPriceValidator));
            When(x => x.SellByType == "mass", () => Include(iUpsertMassProductArgsValidator));
        }
    }
}
