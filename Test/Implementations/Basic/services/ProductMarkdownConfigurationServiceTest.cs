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
            _productMarkdownConfigurationService = DependencyProvider.CreateProductMarkdownConfigurationService();
        }

        [Theory]
        [InlineData(null, "*Markdown product name is required*")]
        [InlineData("", "*Markdown product name is required*")]
        [InlineData(" ", "*Markdown product name is required*")]
        [InlineData("milk", "*Product name \"milk\" does not exist*")]
        public void UpsertProductMarkdown_WithInvalidProductName_ThrowsArgumentException(string productName, string message)
        {
            Action upsertMarkdown = () => _productMarkdownConfigurationService.UpsertProductMarkdown(new UpsertProductMarkdownArgs(productName, 0.1m, _now.StartOfWeek(), _now.EndOfWeek()));

            upsertMarkdown.Should().Throw<ValidationException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*Markdown amount off retail is required*")]
        [InlineData(0, "*Markdown amount off retail must be greater than zero*")]
        [InlineData(10, "*Markdown amount off retail must be less than or equal to product retail price*")]
        public void UpsertProductMarkdown_WithInvalidMarkdownAmountOffRetail_ThrowsArgumentException(double? amountOffRetail, string message)
        {
            Action upsertMarkdown = () => _productMarkdownConfigurationService.UpsertProductMarkdown(new UpsertProductMarkdownArgs("can of soup", (decimal?) amountOffRetail, _now.StartOfWeek(), _now.EndOfWeek()));

            upsertMarkdown.Should().Throw<ValidationException>().WithMessage(message);
        }

        [Theory]
        [ClassData(typeof(InvalidTimeRangeUpsertProductMarkdownData))]
        public void UpsertProductMarkdown_WithInvalidTimeRange_ThrowsArgumentException(DateTime? startTime, DateTime? endTime, string message)
        {
            Action upsertMarkdown = () => _productMarkdownConfigurationService.UpsertProductMarkdown(new UpsertProductMarkdownArgs("can of soup", 0.1m, startTime, endTime));

            upsertMarkdown.Should().Throw<ValidationException>().WithMessage(message);
        }

        public class InvalidTimeRangeUpsertProductMarkdownData : IEnumerable<object[]>
        {
            private readonly DateTime _now = DependencyProvider.CreateDateTimeProvider().Now;

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
