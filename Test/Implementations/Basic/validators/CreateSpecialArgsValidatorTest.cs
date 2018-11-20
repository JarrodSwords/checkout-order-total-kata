using System;
using FluentAssertions;
using FluentValidation;
using FluentValidation.TestHelper;
using PointOfSale.Domain;
using PointOfSale.Implementations.Basic;
using PointOfSale.Services;
using PointOfSale.Test.Infrastructure.InMemory;
using Xunit;

namespace PointOfSale.Test.Implementations.Basic
{
    public class CreateSpecialArgsValidatorTest
    {
        private IDateTimeProvider _dateTimeProvider = DependencyProvider.CreateDateTimeProvider();
        private IProductRepository _productRepository = new InMemoryProductRepositoryFactory().CreateSeededRepository();
        private CreateSpecialArgsValidator _validator;

        public CreateSpecialArgsValidatorTest()
        {
            _validator = new CreateSpecialArgsValidator(
                DependencyProvider.CreateProductNameExistsValidator(_productRepository),
                DependencyProvider.CreateTemporalValidator()
            );
        }

        [Fact]
        public void CreateSpecialArgsValidator_ContainsCorrectValidationRules()
        {
            _validator.ShouldHaveValidationErrorFor(x => x.ProductName, null as string);
            _validator.ShouldHaveValidationErrorFor(x => x.ProductName, "");
            _validator.ShouldHaveValidationErrorFor(x => x.ProductName, " ");
            _validator.ShouldHaveValidationErrorFor(x => x.ProductName, "milk");
            _validator.ShouldHaveValidationErrorFor(x => x.EndTime, (DateTime?) null);

            var args = new CreateSpecialArgs() { ProductName = "can of soup", EndTime = _dateTimeProvider.Now };
            Action validate = () => _validator.ValidateAndThrow(args);
            validate.Should().Throw<ValidationException>("*Special start time is required*");

            args.StartTime = _dateTimeProvider.Now;
            validate.Should().Throw<ValidationException>("*Special start time must be less than end time*");
        }
    }
}
