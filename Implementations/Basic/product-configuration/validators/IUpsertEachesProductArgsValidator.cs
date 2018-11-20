using FluentValidation;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class IUpsertEachesProductArgsValidator : AbstractValidator<IUpsertEachesProductArgs>
    {
        public IUpsertEachesProductArgsValidator()
        {
            RuleFor(x => x.RetailPrice)
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}
