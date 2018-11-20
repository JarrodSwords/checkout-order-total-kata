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
        private CreateSpecialArgsValidator _createSpecialArgsValidator = ValidatorProvider.CreateSpecialArgsValidator();
        private IDateTimeProvider _dateTimeProvider = DependencyProvider.CreateDateTimeProvider();

        [Fact]
        public void CreateSpecialArgsValidator_ContainsCorrectValidationRules()
        {
            _createSpecialArgsValidator.ShouldHaveValidationErrorFor(x => x.ProductName, null as string);
            _createSpecialArgsValidator.ShouldHaveValidationErrorFor(x => x.ProductName, "");
            _createSpecialArgsValidator.ShouldHaveValidationErrorFor(x => x.ProductName, " ");
            _createSpecialArgsValidator.ShouldHaveValidationErrorFor(x => x.ProductName, "milk");
            _createSpecialArgsValidator.ShouldHaveValidationErrorFor(x => x.EndTime, (DateTime?) null);

            var args = new CreateSpecialArgs() { ProductName = "can of soup", EndTime = _dateTimeProvider.Now };
            Action validate = () => _createSpecialArgsValidator.ValidateAndThrow(args);
            validate.Should().Throw<ValidationException>("*Special start time is required*");

            args.StartTime = _dateTimeProvider.Now;
            validate.Should().Throw<ValidationException>("*Special start time must be less than end time*");
        }
    }
}
