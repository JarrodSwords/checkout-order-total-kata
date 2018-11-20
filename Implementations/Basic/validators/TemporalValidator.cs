using FluentValidation;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class TemporalValidator : AbstractValidator<ITemporalArgs>
    {
        public TemporalValidator()
        {
            RuleFor(x => x.StartTime)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .LessThan(x => x.EndTime)
                .When(x => x.EndTime != null)
                .WithMessage("'Start Time' must precede 'End Time'");

            RuleFor(x => x.EndTime)
                .NotNull();
        }
    }
}
