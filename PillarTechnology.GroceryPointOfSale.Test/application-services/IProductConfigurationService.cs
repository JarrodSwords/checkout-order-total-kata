using System;
using System.Collections.Generic;
using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public abstract class IProductConfigurationServiceTest
    {
        protected IProductConfigurationService _productConfigurationService;
        protected IProductService _productService;

        #region Create product

        [Theory]
        [InlineData("milk", 1.99, "Unit")]
        [InlineData("turkey", 1.5, "Weight")]
        public void CreateProduct_CreatesPersistedProduct(string productName, double retailPrice, string sellByType)
        {
            var createProductDto = new UpsertProductDto(productName, (decimal) retailPrice, sellByType);

            var persistedProductDto = _productConfigurationService.CreateProduct(createProductDto);

            persistedProductDto.Should().BeEquivalentTo(createProductDto);
        }

        [Theory]
        [InlineData(null, "*Product name is required*")]
        [InlineData("", "*Product name is required*")]
        [InlineData(" ", "*Product name is required*")]
        [InlineData("can of soup", "*Product name \"can of soup\" already exists*")]
        public void CreateProduct_WithInvalidName_ThrowsArgumentException(string productName, string message)
        {
            var createProductDto = new UpsertProductDto(productName, 1m, "Unit");

            Action createProduct = () => _productConfigurationService.CreateProduct(createProductDto);

            createProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*Product retail price is required*")]
        [InlineData(-1, "*Product retail price cannot be negative*")]
        public void CreateProduct_WithInvalidRetailPrice_ThrowsArgumentException(double? retailPrice, string message)
        {
            var createProductDto = new UpsertProductDto("milk", (decimal?) retailPrice, "Unit");

            Action createProduct = () => _productConfigurationService.CreateProduct(createProductDto);

            createProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*Product sell by type is required*")]
        [InlineData("", "*Product sell by type is required*")]
        [InlineData(" ", "*Product sell by type is required*")]
        [InlineData("Volume", "*Product sell by type \"Volume\" is not in: Unit, Weight*")]
        public void CreateProduct_WithInvalidSellByType_ThrowsArgumentException(string sellByType, string message)
        {
            var createProductDto = new UpsertProductDto("milk", 1.99m, sellByType);

            Action createProduct = () => _productConfigurationService.CreateProduct(createProductDto);

            createProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        #endregion

        #region Update product

        [Theory]
        [InlineData("can of soup", 0.99, "Weight")]
        [InlineData("lean ground beef", 2.5, "Unit")]
        public void UpdateProduct_UpdatesNonIdentityFieldsInPersistedProduct(string productName, double retailPrice, string sellByType)
        {
            var updateProductDto = new UpsertProductDto(productName, (decimal) retailPrice, sellByType);

            var persistedProductDto = _productConfigurationService.UpdateProduct(updateProductDto);

            persistedProductDto.Should().BeEquivalentTo(updateProductDto);
        }

        [Theory]
        [InlineData(null, "*Product name is required*")]
        [InlineData("", "*Product name is required*")]
        [InlineData(" ", "*Product name is required*")]
        [InlineData("milk", "*Product name \"milk\" does not exist*")]
        public void UpdateProduct_WithInvalidName_ThrowsArgumentException(string productName, string message)
        {
            var updateProductDto = new UpsertProductDto(productName, 1m, "Unit");

            Action updateProduct = () => _productConfigurationService.UpdateProduct(updateProductDto);

            updateProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*Product retail price is required*")]
        [InlineData(-1, "*Product retail price cannot be negative*")]
        public void UpdateProduct_WithInvalidRetailPrice_ThrowsArgumentException(double? retailPrice, string message)
        {
            var updateProductDto = new UpsertProductDto("milk", (decimal?) retailPrice, "Unit");

            Action updateProduct = () => _productConfigurationService.UpdateProduct(updateProductDto);

            updateProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*Product sell by type is required*")]
        [InlineData("", "*Product sell by type is required*")]
        [InlineData(" ", "*Product sell by type is required*")]
        [InlineData("Volume", "*Product sell by type \"Volume\" is not in: Unit, Weight*")]
        public void UpdateProduct_WithInvalidSellByType_ThrowsArgumentException(string sellByType, string message)
        {
            var updateProductDto = new UpsertProductDto("milk", 1.99m, sellByType);

            Action updateProduct = () => _productConfigurationService.UpdateProduct(updateProductDto);

            updateProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        #endregion

        #region 

        [Theory]
        [MemberData(nameof(UpsertProductMarkdownData))]
        public void UpsertProductMarkdown_UpsertsProductMarkdown(string productName, decimal amountOffRetail, DateTime startTime, DateTime endTime)
        {
            var updateProductMarkdownDto = new UpsertProductMarkdownDto(productName, amountOffRetail, startTime, endTime);

            var persistedProduct = _productConfigurationService.UpsertProductMarkdown(updateProductMarkdownDto);

            persistedProduct.Markdown.AmountOffRetail.Should().Be(amountOffRetail);
            persistedProduct.Markdown.StartTime.Should().Be(startTime);
            persistedProduct.Markdown.EndTime.Should().Be(endTime);
        }

        public static IEnumerable<object[]> UpsertProductMarkdownData => new List<object[]>
        {
            new object[] { "can of soup", 0.1m, DateTimeProvider.Now().StartOfWeek(), DateTimeProvider.Now().EndOfWeek() },
            new object[] { "can of soup", 0.1m, DateTimeProvider.Now().StartOfLastWeek(), DateTimeProvider.Now().EndOfLastWeek() },
            new object[] { "can of soup", 0.1m, DateTimeProvider.Now().StartOfNextWeek(), DateTimeProvider.Now().EndOfNextWeek() }
        };

        #endregion
    }
}