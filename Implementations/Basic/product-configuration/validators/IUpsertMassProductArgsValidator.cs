using FluentValidation;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class IUpsertMassProductArgsValidator : AbstractValidator<IUpsertMassProductArgs>
    {
        public IUpsertMassProductArgsValidator()
        {
            RuleFor(x => x.RetailPricePerUnit)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}
