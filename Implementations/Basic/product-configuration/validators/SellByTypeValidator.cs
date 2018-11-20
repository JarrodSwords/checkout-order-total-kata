using System.Linq;
using FluentValidation;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class SellByTypeValidator : AbstractValidator<IUpsertProductArgs>
    {
        public SellByTypeValidator(IProductFactoryProvider productFactoryProvider)
        {
            var sellByTypes = productFactoryProvider.GetSellByTypes();

            RuleFor(x => x.SellByType).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(x => sellByTypes.Contains(x))
                .WithMessage($"'{{PropertyName}}' \"{{PropertyValue}}\" is not in: {string.Join(", ", sellByTypes)}");
        }
    }
}
