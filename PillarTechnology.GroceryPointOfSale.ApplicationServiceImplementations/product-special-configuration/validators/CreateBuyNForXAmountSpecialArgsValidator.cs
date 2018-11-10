using FluentValidation;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public class CreateBuyNForXAmountSpecialArgsValidator : AbstractValidator<CreateBuyNForXAmountSpecialArgs>
    {
        private readonly IProductRepository _productRepository;

        public CreateBuyNForXAmountSpecialArgsValidator(CreateSpecialArgsValidator _createSpecialArgsValidator)
        {
            Include(_createSpecialArgsValidator);
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.DiscountedItems)
                .NotNull().WithMessage("Special discounted items is required")
                .GreaterThan(0).WithMessage("Special discounted items must be greater than zero");

            RuleFor(x => x.GroupSalePrice)
                .NotNull().WithMessage("Group sale price is required")
                .GreaterThan(0).WithMessage("Group sale price must be greater than zero");
        }
    }
}
