using System;
using System.Linq;
using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
{
    public class CreateProductArgsValidator : AbstractValidator<UpsertProductArgs>
    {
        private readonly IProductFactoryProvider _productFactoryProvider;
        private readonly IProductRepository _productRepository;

        public CreateProductArgsValidator(IProductFactoryProvider productFactoryProvider, IProductRepository productRepository)
        {
            _productFactoryProvider = productFactoryProvider;
            _productRepository = productRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            var sellByTypes = _productFactoryProvider.GetSellByTypes();

            ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required")
                .Must(x => !_productRepository.Exists(x)).WithMessage("Product name \"{PropertyValue}\" already exists");

            RuleFor(x => x.SellByType)
                .NotEmpty().WithMessage("Product sell by type is required")
                .Must(x => sellByTypes.Contains(x)).WithMessage($"Product sell by type \"{{PropertyValue}}\" is not in: {string.Join(", ", sellByTypes)}");

            When(x => x.SellByType == "eaches", () =>
            {
                RuleFor(x => x.RetailPrice)
                    .NotNull().WithMessage("Product retail price is required")
                    .GreaterThanOrEqualTo(0).WithMessage("Product retail price cannot be negative");
            });

            When(x => x.SellByType == "mass", () =>
            {
                RuleFor(x => x.RetailPricePerUnit)
                    .NotNull().WithMessage("Product retail price per unit is required")
                    .GreaterThanOrEqualTo(0).WithMessage("Product retail price per unit cannot be negative");
            });
        }
    }
}
