using System;
using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class CreateBuyNForXAmountSpecialArgsValidatorTest
    {
        private IProductRepository _productRepository = DependencyProvider.CreateProductRepository();
        private CreateBuyNForXAmountSpecialArgsValidator _validator;

        public CreateBuyNForXAmountSpecialArgsValidatorTest()
        {
            var baseValidator = new CreateSpecialArgsValidator(_productRepository);
            _validator = new CreateBuyNForXAmountSpecialArgsValidator(_productRepository, baseValidator);
        }

        [Fact]
        public void CreateBuyNForXAmountSpecialArgsValidator_ContainsCorrectValidationRules()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ProductName, null as string);
            _validator.ShouldHaveValidationErrorFor(x => x.ProductName, "");
            _validator.ShouldHaveValidationErrorFor(x => x.ProductName, " ");
            _validator.ShouldHaveValidationErrorFor(x => x.ProductName, "milk");
            _validator.ShouldHaveValidationErrorFor(x => x.EndTime, (DateTime?) null);
            _validator.ShouldHaveValidationErrorFor(x => x.DiscountedItems, (int?) null);
            _validator.ShouldHaveValidationErrorFor(x => x.DiscountedItems, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.GroupSalePrice, (decimal?) null);
            _validator.ShouldHaveValidationErrorFor(x => x.GroupSalePrice, 0);

            var args = new CreateBuyNForXAmountSpecialArgs() { ProductName = "can of soup", EndTime = DateTime.Now };
            Action validate = () => _validator.ValidateAndThrow(args);
            validate.Should().Throw<ValidationException>("*Special start time is required*");

            args.StartTime = DateTime.Now;
            validate.Should().Throw<ValidationException>("*Special start time must be less than end time*");

            args.ProductName = "lean ground beef";
            args.EndTime = DateTime.Now;
            validate.Should().Throw<ValidationException>("*Special can only be applied to a product with the Unit sell by type*");
        }
    }
}
