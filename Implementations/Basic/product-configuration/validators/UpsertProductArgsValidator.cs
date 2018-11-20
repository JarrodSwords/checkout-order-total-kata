using FluentValidation;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public abstract class UpsertProductArgsValidator : AbstractValidator<UpsertProductArgs>
    {
        public UpsertProductArgsValidator(
            SellByTypeValidator sellByTypeValidator,
            IUpsertEachesProductArgsValidator iUpsertEachesProductArgsValidator,
            IUpsertMassProductArgsValidator iUpsertMassProductArgsValidator
        )
        {
            Include(sellByTypeValidator);
            When(x => x.SellByType == "eaches", () => Include(iUpsertEachesProductArgsValidator));
            When(x => x.SellByType == "mass", () => Include(iUpsertMassProductArgsValidator));
        }
    }
}
