using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
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
                .Must(x => _productRepository.FindProduct(x).GetType() == typeof(EachesProduct))
                .WithMessage("Product name \"{PropertyValue}\" cannot be sold as eaches");
        }
    }
}
