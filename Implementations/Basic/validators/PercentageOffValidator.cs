using FluentValidation;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class PercentageOffValidator : AbstractValidator<IPercentageOffArgs>
    {
        public PercentageOffValidator()
        {
            RuleFor(x => x.PercentageOff)
                .NotNull()
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}
