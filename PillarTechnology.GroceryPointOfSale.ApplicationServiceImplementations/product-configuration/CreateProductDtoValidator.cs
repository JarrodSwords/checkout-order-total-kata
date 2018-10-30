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
                .NotEmpty().WithMessage("Product name is required")
                .Must(x => !_productRepository.Exists(x)).WithMessage("Product \"{PropertyValue}\" already exists");

            RuleFor(x => x.RetailPrice).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Product retail price is required")
                .GreaterThanOrEqualTo(0).WithMessage("Product retail price cannot be negative");
        }
    }
}