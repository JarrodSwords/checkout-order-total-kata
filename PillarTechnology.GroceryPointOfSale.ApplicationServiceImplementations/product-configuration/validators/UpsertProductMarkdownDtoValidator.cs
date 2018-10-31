using System;
using System.Linq;
using FluentValidation;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public class UpsertProductMarkdownDtoValidator : AbstractValidator<UpsertProductMarkdownDto>
    {
        private readonly IProductRepository _productRepository;

        public UpsertProductMarkdownDtoValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.ProductName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Markdown product name is required")
                .Must(x => _productRepository.Exists(x)).WithMessage("Product name \"{PropertyValue}\" does not exist");

        }
    }
}