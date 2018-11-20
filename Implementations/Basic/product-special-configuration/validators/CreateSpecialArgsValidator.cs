using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class CreateSpecialArgsValidator : AbstractValidator<CreateSpecialArgs>
    {
        public CreateSpecialArgsValidator(
            DiscountedItemsValidator discountedItemsValidator,
            GroupSalePriceValidator groupSalePriceValidator,
            IsEachesProductValidator isEachesProductValidator,
            IsMassProductValidator isMassProductValidator,
            PercentageOffValidator percentageOffValidator,
            PreDiscountItemsValidator preDiscountItemsValidator,
            ProductMustExistValidator productMustExistValidator,
            TemporalValidator temporalValidator
        )
        {
            Include(discountedItemsValidator);
            Include(productMustExistValidator);
            Include(temporalValidator);

            When(x => x.SpecialType == "BuyNForXAmount", () =>
            {
                Include(groupSalePriceValidator);
                Include(isEachesProductValidator);
            });

            When(x => x.SpecialType == "BuyNGetMAtXPercentOff", () =>
            {
                Include(isEachesProductValidator);
                Include(percentageOffValidator);
                Include(preDiscountItemsValidator);
            });

            When(x => x.SpecialType == "BuyNGetMOfEqualOrLesserValueAtXPercentOff", () =>
            {
                Include(isMassProductValidator);
                Include(percentageOffValidator);
                Include(preDiscountItemsValidator);
            });
        }
    }
}
