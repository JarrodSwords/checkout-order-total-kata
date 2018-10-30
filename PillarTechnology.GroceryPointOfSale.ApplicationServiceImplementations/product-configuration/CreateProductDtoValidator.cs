using FluentValidation;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductDtoValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.Name).Cascade(CascadeMode.StopOnFirstFailure)
                .Must(x => !_productRepository.Exists(x)).WithMessage(ErrorMessages.GetMessage(Error.CannotCreateExistingProduct))
                .NotEmpty().WithMessage(ErrorMessages.GetMessage(Error.ProductNameRequired));

            RuleFor(x => x.RetailPrice).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage(ErrorMessages.GetMessage(Error.ProductRetailPriceRequired))
                .GreaterThanOrEqualTo(0).WithMessage(ErrorMessages.GetMessage(Error.ProductRetailPriceCannotBeNegative));
        }
    }
}