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
    public class CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidatorTest
    {
        private IDateTimeProvider _dateTimeProvider = DependencyProvider.CreateDateTimeProvider();
        private IProductRepository _productRepository = new InMemoryProductRepositoryFactory().CreateSeededRepository();
        private CreateBuyNGetMAtXPercentOffSpecialArgsValidator _validator;

        public CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidatorTest()
        {
            var baseValidator = new CreateSpecialArgsValidator(_productRepository);
            _validator = new CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialArgsValidator(_productRepository, baseValidator);
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

            var args = new CreateBuyNGetMAtXPercentOffSpecialArgs() { ProductName = "can of soup", EndTime = _dateTimeProvider.Now };
            Action validate = () => _validator.ValidateAndThrow(args);
            validate.Should().Throw<ValidationException>("*Special start time is required*");

            args.StartTime = _dateTimeProvider.Now;
            validate.Should().Throw<ValidationException>("*Special start time must be less than end time*");

            args.EndTime = _dateTimeProvider.Now;
            validate.Should().Throw<ValidationException>("*Special can only be applied to a product with the Weight sell by type*");
        }
    }
}
