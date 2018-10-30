using System;
using System.Linq;
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
            var sellByTypes = Enum.GetNames(typeof(SellByType));

            RuleFor(x => x.Name).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Product name is required")
                .Must(x => !_productRepository.Exists(x)).WithMessage("Product name \"{PropertyValue}\" already exists");

            RuleFor(x => x.RetailPrice).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("Product retail price is required")
                .GreaterThanOrEqualTo(0).WithMessage("Product retail price cannot be negative");

            RuleFor(x => x.SellByType)
                .NotEmpty().WithMessage("Product sell by type is required")
                .Must(x => sellByTypes.Contains(x)).WithMessage($"Product sell by type \"{{PropertyValue}}\" is not in: {string.Join(", ", sellByTypes)}");
        }
    }
}