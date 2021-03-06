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
    public class CreateBuyNGetMAtXPercentOffSpecialArgsValidatorTest
    {
        private IProductRepository _productRepository = new InMemoryProductRepositoryFactory().CreateSeededRepository();
        private CreateBuyNGetMAtXPercentOffSpecialArgsValidator _validator;

        public CreateBuyNGetMAtXPercentOffSpecialArgsValidatorTest()
        {
            var baseValidator = new CreateSpecialArgsValidator(_productRepository);
            _validator = new CreateBuyNGetMAtXPercentOffSpecialArgsValidator(_productRepository, baseValidator);
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
            _validator.ShouldHaveValidationErrorFor(x => x.PercentageOff, (decimal?) null);
            _validator.ShouldHaveValidationErrorFor(x => x.PercentageOff, 0);
            _validator.ShouldHaveValidationErrorFor(x => x.PercentageOff, 101);
            _validator.ShouldHaveValidationErrorFor(x => x.PreDiscountItems, (int?) null);
            _validator.ShouldHaveValidationErrorFor(x => x.PreDiscountItems, 0);

            var args = new CreateBuyNGetMAtXPercentOffSpecialArgs() { ProductName = "can of soup", EndTime = DateTime.Now };
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
