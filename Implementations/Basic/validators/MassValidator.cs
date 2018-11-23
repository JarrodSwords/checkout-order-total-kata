using System;
using System.Linq;
using FluentValidation;
using PointOfSale.Services;
using UnitsNet.Units;

namespace PointOfSale.Implementations.Basic
{
    public class MassValidator : AbstractValidator<IMassArgs>
    {
        public MassValidator()
        {
            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            var massUnitTypes = Enum.GetNames(typeof(MassUnit));

            RuleFor(x => x.MassAmount)
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.MassUnit)
                .Must(x => massUnitTypes.Contains(x))
                .WithMessage("\"{PropertyValue}\" is not a valid {PropertyName}")
                .When(x => !String.IsNullOrWhiteSpace(x.MassUnit));
        }
    }
}
