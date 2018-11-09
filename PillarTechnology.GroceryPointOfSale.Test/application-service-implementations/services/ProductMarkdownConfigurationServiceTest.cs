using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class ProductMarkdownConfigurationServiceTest : IProductMarkdownConfigurationServiceTest
    {
        public ProductMarkdownConfigurationServiceTest()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
            var productRepository = new InMemoryProductRepositoryFactory().CreateSeededRepository();
            var upsertProductMarkdownArgsValidator = new UpsertProductMarkdownArgsValidator(productRepository);

            _productMarkdownConfigurationService = new ProductMarkdownConfigurationService(mapper, productRepository, upsertProductMarkdownArgsValidator);
        }

        [Theory]
        [InlineData(null, "*Markdown product name is required*")]
        [InlineData("", "*Markdown product name is required*")]
        [InlineData(" ", "*Markdown product name is required*")]
        [InlineData("milk", "*Product name \"milk\" does not exist*")]
        public void UpsertProductMarkdown_WithInvalidProductName_ThrowsArgumentException(string productName, string message)
        {
            Action upsertMarkdown = () => _productMarkdownConfigurationService.UpsertProductMarkdown(new UpsertProductMarkdownArgs(productName, 0.1m, _now.StartOfWeek(), _now.EndOfWeek()));

            upsertMarkdown.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*Markdown amount off retail is required*")]
        [InlineData(0, "*Markdown amount off retail must be greater than zero*")]
        [InlineData(10, "*Markdown amount off retail must be less than or equal to product retail price*")]
        public void UpsertProductMarkdown_WithInvalidMarkdownAmountOffRetail_ThrowsArgumentException(double? amountOffRetail, string message)
        {
            Action upsertMarkdown = () => _productMarkdownConfigurationService.UpsertProductMarkdown(new UpsertProductMarkdownArgs("can of soup", (decimal?) amountOffRetail, _now.StartOfWeek(), _now.EndOfWeek()));

            upsertMarkdown.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [ClassData(typeof(InvalidTimeRangeUpsertProductMarkdownData))]
        public void UpsertProductMarkdown_WithInvalidTimeRange_ThrowsArgumentException(DateTime? startTime, DateTime? endTime, string message)
        {
            Action upsertMarkdown = () => _productMarkdownConfigurationService.UpsertProductMarkdown(new UpsertProductMarkdownArgs("can of soup", 0.1m, startTime, endTime));

            upsertMarkdown.Should().Throw<ArgumentException>().WithMessage(message);
        }

        public class InvalidTimeRangeUpsertProductMarkdownData : IEnumerable<object[]>
        {
            private readonly DateTime _now = DateTime.Now;

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { null, _now.EndOfWeek(), "Markdown start time is required" };
                yield return new object[] { _now.StartOfWeek(), null, "Markdown end time is required" };
                yield return new object[] { _now.EndOfWeek(), _now.StartOfWeek(), "Markdown start time must be less than end time" };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
