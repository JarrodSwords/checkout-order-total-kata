using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator : CreateBuyNGetMAtXPercentOffSpecialArgsValidator
    {
        public CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator(
            IProductRepository productRepository,
            CreateSpecialArgsValidator createSpecialArgsValidator
        ) : base(productRepository, createSpecialArgsValidator) { }

        protected override void CreateProductValidation()
        {
            RuleFor(x => x.ProductName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Product name is required")
                .Must(x => _productRepository.Exists(x)).WithMessage("Product name \"{PropertyValue}\" does not exist")
                .Must(x => _productRepository.FindProduct(x).SellByType == SellByType.Weight)
                .WithMessage("Special can only be applied to a product with the Weight sell by type");
        }
    }
}
