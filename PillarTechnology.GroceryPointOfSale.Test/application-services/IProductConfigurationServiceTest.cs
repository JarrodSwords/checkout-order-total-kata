using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public abstract class IProductConfigurationServiceTest
    {
        protected readonly DateTime _now = DateTime.Now;
        protected IProductConfigurationService _productConfigurationService;
        protected IProductService _productService;

        [Theory]
        [InlineData("milk", 1.99, "Unit")]
        [InlineData("turkey", 1.5, "Weight")]
        public void CreateProduct_CreatesPersistedProduct(string productName, double retailPrice, string sellByType)
        {
            var args = new UpsertProductArgs(productName, (decimal) retailPrice, sellByType);

            var persistedProductDto = _productConfigurationService.CreateProduct(args);

            persistedProductDto.Name.Should().Be(args.Name);
            persistedProductDto.RetailPrice.Should().Be(args.RetailPrice);
            persistedProductDto.SellByType.Should().Be(args.SellByType);
        }

        [Theory]
        [InlineData("can of soup", 0.99, "Weight")]
        [InlineData("lean ground beef", 2.5, "Unit")]
        public void UpdateProduct_UpdatesNonIdentityFieldsInPersistedProduct(string productName, double retailPrice, string sellByType)
        {
            var args = new UpsertProductArgs(productName, (decimal) retailPrice, sellByType);

            var persistedProductDto = _productConfigurationService.UpdateProduct(args);

            persistedProductDto.Name.Should().Be(args.Name);
            persistedProductDto.RetailPrice.Should().Be(args.RetailPrice);
            persistedProductDto.SellByType.Should().Be(args.SellByType);
        }

        [Theory]
        [ClassData(typeof(UpsertProductMarkdownData))]
        public void UpsertProductMarkdown_UpsertsProductMarkdown(string productName, decimal amountOffRetail, DateTime? startTime, DateTime? endTime)
        {
            var args = new UpsertProductMarkdownArgs(productName, amountOffRetail, startTime, endTime);

            var persistedProduct = _productConfigurationService.UpsertProductMarkdown(args);

            persistedProduct.Name.Should().Be(args.ProductName);
            persistedProduct.Markdown.AmountOffRetail.Should().Be(args.AmountOffRetail);
            persistedProduct.Markdown.StartTime.Should().Be(args.StartTime.Value);
            persistedProduct.Markdown.EndTime.Should().Be(args.EndTime.Value);
        }

        #region Test data

        public class UpsertProductMarkdownData : IEnumerable<object[]>
        {
            private readonly DateTime _now = DateTime.Now;

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "can of soup", 0.1m, _now.StartOfWeek(), _now.EndOfWeek() };
                yield return new object[] { "lean ground beef", 0.1m, _now.StartOfWeek(), _now.EndOfWeek() };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        #endregion
    }
}