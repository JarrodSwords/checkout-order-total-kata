using FluentValidation;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public class CreateBuyNGetMAtXPercentOffSpecialArgsValidator : AbstractValidator<CreateBuyNGetMAtXPercentOffSpecialArgs>
    {
        public CreateBuyNGetMAtXPercentOffSpecialArgsValidator(CreateSpecialArgsValidator _createSpecialArgsValidator)
        {
            Include(_createSpecialArgsValidator);
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.DiscountedItems)
                .NotNull().WithMessage("Special discounted items is required")
                .GreaterThan(0).WithMessage("Special discounted items must be greater than zero");

            RuleFor(x => x.PercentageOff)
                .NotNull().WithMessage("Special percentage off is required")
                .GreaterThan(0).WithMessage("Special percentage off must be greater than zero")
                .LessThanOrEqualTo(100).WithMessage("Special percentage off must be less than or equal to 100");

            RuleFor(x => x.PreDiscountItems)
                .NotNull().WithMessage("Special pre-discount items is required")
                .GreaterThan(0).WithMessage("Special pre-discount items must be greater than zero");
        }
    }
}
