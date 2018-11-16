using FluentValidation;
using PointOfSale.ApplicationServices;
using PointOfSale.Domain;

namespace PointOfSale.ApplicationServiceImplementations
{
    public class ScanWeightedItemArgsValidator : AbstractValidator<ScanWeightedItemArgs>
    {
        private IProductRepository _productRepository;

        public ScanWeightedItemArgsValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.ProductName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Product name is required")
                .Must(x => _productRepository.Exists(x)).WithMessage("Product name \"{PropertyValue}\" does not exist")
                .Must(x => _productRepository.FindProduct(x).SellByType == SellByType.Weight)
                .WithMessage("Product name \"{PropertyValue}\" cannot be sold by weight");
        }
    }
}