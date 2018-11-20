using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using FluentValidation;
using PointOfSale.Domain;
using PointOfSale.Services;
using PointOfSale.Test.Services;
using Xunit;

namespace PointOfSale.Test.Implementations.Basic
{
    public class ProductMarkdownConfigurationServiceTest : IProductMarkdownConfigurationServiceTest
    {
        public ProductMarkdownConfigurationServiceTest()
        {
            _productMarkdownConfigurationService = DependencyProvider.ProductMarkdownConfigurationService();
        }

        [Theory]
        [InlineData(null, "*'Product name' should not be empty*")]
        [InlineData("", "*'Product name' should not be empty*")]
        [InlineData(" ", "*'Product name' should not be empty*")]
        [InlineData("milk", "*'Product name' \"milk\" does not exist*")]
        public void UpsertProductMarkdown_WithInvalidProductName_ThrowsArgumentException(string productName, string message)
        {
            Action upsertMarkdown = () => _productMarkdownConfigurationService.UpsertProductMarkdown(
                new UpsertProductMarkdownArgs(
                    0.1m,
                    _now.EndOfWeek(),
                    productName,
                    "eaches",
                    _now.StartOfWeek()
                )
            );

            upsertMarkdown.Should().Throw<ValidationException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*'Amount Off Retail' must not be empty*")]
        [InlineData(0, "*'Amount Off Retail' must be greater than '0'*")]
        [InlineData(10, "*Amount Off Retail must be less than or equal to 'Retail Price'*")]
        public void UpsertProductMarkdown_WithInvalidMarkdownAmountOffRetail_ThrowsArgumentException(double? amountOffRetail, string message)
        {
            Action upsertMarkdown = () => _productMarkdownConfigurationService.UpsertProductMarkdown(
                new UpsertProductMarkdownArgs(
                    (decimal?) amountOffRetail,
                    _now.EndOfWeek(),
                    "can of soup",
                    "eaches",
                    _now.StartOfWeek()
                )
            );

            upsertMarkdown.Should().Throw<ValidationException>().WithMessage(message);
        }

        [Theory]
        [ClassData(typeof(InvalidTimeRangeUpsertProductMarkdownData))]
        public void UpsertProductMarkdown_WithInvalidTimeRange_ThrowsArgumentException(DateTime? startTime, DateTime? endTime, string message)
        {
            Action upsertMarkdown = () => _productMarkdownConfigurationService.UpsertProductMarkdown(
                new UpsertProductMarkdownArgs(
                    0.1m,
                    endTime,
                    "can of soup",
                    "eaches",
                    startTime
                )
            );

            upsertMarkdown.Should().Throw<ValidationException>().WithMessage(message);
        }

        public class InvalidTimeRangeUpsertProductMarkdownData : IEnumerable<object[]>
        {
            private readonly DateTime _now = DependencyProvider.DateTimeProvider().Now;

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { null, _now.EndOfWeek(), "*'Start Time' must not be empty*" };
                yield return new object[] { _now.StartOfWeek(), null, "*'End Time' must not be empty*" };
                yield return new object[] { _now.EndOfWeek(), _now.StartOfWeek(), "*'Start Time' must precede 'End Time'*" };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
