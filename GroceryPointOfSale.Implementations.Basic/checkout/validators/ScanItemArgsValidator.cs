using FluentValidation;
using GroceryPointOfSale.ApplicationServices;
using GroceryPointOfSale.Domain;

namespace GroceryPointOfSale.ApplicationServiceImplementations
{
    public class ScanItemArgsValidator : AbstractValidator<ScanItemArgs>
    {
        private IProductRepository _productRepository;

        public ScanItemArgsValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.ProductName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Product name is required")
                .Must(x => _productRepository.Exists(x)).WithMessage("Product name \"{PropertyValue}\" does not exist")
                .Must(x => _productRepository.FindProduct(x).SellByType == SellByType.Unit)
                .WithMessage("Product name \"{PropertyValue}\" cannot be sold by unit");
        }
    }
}