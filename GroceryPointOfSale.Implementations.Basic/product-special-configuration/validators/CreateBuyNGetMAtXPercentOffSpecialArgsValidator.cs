using FluentValidation;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public class CreateBuyNGetMAtXPercentOffSpecialArgsValidator : AbstractValidator<CreateSpecialArgs>
    {
        protected readonly IProductRepository _productRepository;

        public CreateBuyNGetMAtXPercentOffSpecialArgsValidator(IProductRepository productRepository, CreateSpecialArgsValidator createSpecialArgsValidator)
        {
            _productRepository = productRepository;
            Include(createSpecialArgsValidator);
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

            CreateProductValidation();
        }

        protected virtual void CreateProductValidation()
        {
            RuleFor(x => x.ProductName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Product name is required")
                .Must(x => _productRepository.Exists(x)).WithMessage("Product name \"{PropertyValue}\" does not exist")
                .Must(x => _productRepository.FindProduct(x).SellByType == SellByType.Unit)
                .WithMessage("Special can only be applied to a product with the Unit sell by type");
        }
    }
}
