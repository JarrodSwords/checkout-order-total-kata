using System;
using System.Linq;
using FluentValidation;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations
{
    public class CreateBuyNGetMAtXPercentOffArgsValidator : AbstractValidator<CreateBuyNGetMAtXPercentOffSpecialArgs>
    {
        private readonly IProductRepository _productRepository;

        public CreateBuyNGetMAtXPercentOffArgsValidator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            CreateRules();
        }

        private void CreateRules()
        {
            RuleFor(x => x.ProductName).Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage("Product name is required")
                .Must(x => _productRepository.Exists(x)).WithMessage("Product name \"{PropertyValue}\" does not exist");
        }
    }
}