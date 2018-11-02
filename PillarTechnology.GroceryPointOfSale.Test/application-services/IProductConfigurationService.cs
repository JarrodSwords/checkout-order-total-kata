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
        private readonly DateTime _now = DateTime.Now;
        protected IProductConfigurationService _productConfigurationService;
        protected IProductService _productService;

        #region Create product

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
        [InlineData(null, "*Product name is required*")]
        [InlineData("", "*Product name is required*")]
        [InlineData(" ", "*Product name is required*")]
        [InlineData("can of soup", "*Product name \"can of soup\" already exists*")]
        public void CreateProduct_WithInvalidName_ThrowsArgumentException(string productName, string message)
        {
            Action createProduct = () => _productConfigurationService.CreateProduct(new UpsertProductArgs(productName, 1m, "Unit"));

            createProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*Product retail price is required*")]
        [InlineData(-1, "*Product retail price cannot be negative*")]
        public void CreateProduct_WithInvalidRetailPrice_ThrowsArgumentException(double? retailPrice, string message)
        {
            Action createProduct = () => _productConfigurationService.CreateProduct(new UpsertProductArgs("milk", (decimal?) retailPrice, "Unit"));

            createProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*Product sell by type is required*")]
        [InlineData("", "*Product sell by type is required*")]
        [InlineData(" ", "*Product sell by type is required*")]
        [InlineData("Volume", "*Product sell by type \"Volume\" is not in: Unit, Weight*")]
        public void CreateProduct_WithInvalidSellByType_ThrowsArgumentException(string sellByType, string message)
        {
            Action createProduct = () => _productConfigurationService.CreateProduct(new UpsertProductArgs("milk", 1.99m, sellByType));

            createProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        #endregion

        #region Update product

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
        [InlineData(null, "*Product name is required*")]
        [InlineData("", "*Product name is required*")]
        [InlineData(" ", "*Product name is required*")]
        [InlineData("milk", "*Product name \"milk\" does not exist*")]
        public void UpdateProduct_WithInvalidName_ThrowsArgumentException(string productName, string message)
        {
            Action updateProduct = () => _productConfigurationService.UpdateProduct(new UpsertProductArgs(productName, 1m, "Unit"));

            updateProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*Product retail price is required*")]
        [InlineData(-1, "*Product retail price cannot be negative*")]
        public void UpdateProduct_WithInvalidRetailPrice_ThrowsArgumentException(double? retailPrice, string message)
        {
            Action updateProduct = () => _productConfigurationService.UpdateProduct(new UpsertProductArgs("milk", (decimal?) retailPrice, "Unit"));

            updateProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*Product sell by type is required*")]
        [InlineData("", "*Product sell by type is required*")]
        [InlineData(" ", "*Product sell by type is required*")]
        [InlineData("Volume", "*Product sell by type \"Volume\" is not in: Unit, Weight*")]
        public void UpdateProduct_WithInvalidSellByType_ThrowsArgumentException(string sellByType, string message)
        {
            Action updateProduct = () => _productConfigurationService.UpdateProduct(new UpsertProductArgs("milk", 1.99m, sellByType));

            updateProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        #endregion

        #region Upsert markdown

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

        [Theory]
        [InlineData(null, "*Markdown product name is required*")]
        [InlineData("", "*Markdown product name is required*")]
        [InlineData(" ", "*Markdown product name is required*")]
        [InlineData("milk", "*Product name \"milk\" does not exist*")]
        public void UpsertProductMarkdown_WithInvalidProductName_ThrowsArgumentException(string productName, string message)
        {
            Action upsertMarkdown = () => _productConfigurationService.UpsertProductMarkdown(new UpsertProductMarkdownArgs(productName, 0.1m, _now.StartOfWeek(), _now.EndOfWeek()));

            upsertMarkdown.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*Markdown amount off retail is required*")]
        [InlineData(0, "*Markdown amount off retail must be greater than zero*")]
        [InlineData(10, "*Markdown amount off retail must be less than or equal to product retail price*")]
        public void UpsertProductMarkdown_WithInvalidMarkdownAmountOffRetail_ThrowsArgumentException(double? amountOffRetail, string message)
        {
            Action upsertMarkdown = () => _productConfigurationService.UpsertProductMarkdown(new UpsertProductMarkdownArgs("can of soup", (decimal?) amountOffRetail, _now.StartOfWeek(), _now.EndOfWeek()));

            upsertMarkdown.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [ClassData(typeof(InvalidTimeRangeUpsertProductMarkdownData))]
        public void UpsertProductMarkdown_WithInvalidTimeRange_ThrowsArgumentException(DateTime? startTime, DateTime? endTime, string message)
        {
            Action upsertMarkdown = () => _productConfigurationService.UpsertProductMarkdown(new UpsertProductMarkdownArgs("can of soup", 0.1m, startTime, endTime));

            upsertMarkdown.Should().Throw<ArgumentException>().WithMessage(message);
        }

        #endregion

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

        #endregion
    }
}