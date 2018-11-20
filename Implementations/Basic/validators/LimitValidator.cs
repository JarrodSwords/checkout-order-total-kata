using FluentValidation;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class LimitValidator : AbstractValidator<ILimitArgs>
    {
        public LimitValidator()
        {
            RuleFor(x => x.Limit)
                .GreaterThan(0)
                .When(x => x.Limit.HasValue);
        }
    }
}
