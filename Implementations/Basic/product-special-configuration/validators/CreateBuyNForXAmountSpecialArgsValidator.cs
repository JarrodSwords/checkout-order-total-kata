using FluentValidation;
using PointOfSale.ApplicationServices;
using PointOfSale.Domain;

namespace PointOfSale.ApplicationServiceImplementations
{
    public class CreateBuyNForXAmountSpecialArgsValidator : AbstractValidator<CreateSpecialArgs>
    {
        private readonly IProductRepository _productRepository;

        public CreateBuyNForXAmountSpecialArgsValidator(IProductRepository productRepository, CreateSpecialArgsValidator _createSpecialArgsValidator)
        {
            _productRepository = productRepository;
            Include(_createSpecialArgsValidator);
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.ProductName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Product name is required")
                .Must(x => _productRepository.Exists(x)).WithMessage("Product name \"{PropertyValue}\" does not exist")
                .Must(x => _productRepository.FindProduct(x).SellByType == SellByType.Unit)
                .WithMessage("Special can only be applied to a product with the Unit sell by type");

            RuleFor(x => x.DiscountedItems)
                .NotNull().WithMessage("Special discounted items is required")
                .GreaterThan(0).WithMessage("Special discounted items must be greater than zero");

            RuleFor(x => x.GroupSalePrice)
                .NotNull().WithMessage("Group sale price is required")
                .GreaterThan(0).WithMessage("Group sale price must be greater than zero");
        }
    }
}
