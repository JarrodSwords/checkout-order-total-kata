using System;
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

            Action addProductWithInvalidName = () => _productConfigurationService.CreateProduct(createProductDto);

            addProductWithInvalidName.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*Product retail price is required*")]
        [InlineData(-1, "*Product retail price cannot be negative*")]
        public void CreateProduct_WithInvalidRetailPrice_ThrowsArgumentException(double? retailPrice, string message)
        {
            var createProductDto = new UpsertProductDto("milk", (decimal?) retailPrice, "Unit");

            Action addProductWithInvalidRetailPrice = () => _productConfigurationService.CreateProduct(createProductDto);

            addProductWithInvalidRetailPrice.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*Product sell by type is required*")]
        [InlineData("", "*Product sell by type is required*")]
        [InlineData(" ", "*Product sell by type is required*")]
        [InlineData("Volume", "*Product sell by type \"Volume\" is not in: Unit, Weight*")]
        public void CreateProduct_WithInvalidSellByType_ThrowsArgumentException(string sellByType, string message)
        {
            var createProductDto = new UpsertProductDto("milk", 1.99m, sellByType);

            Action addProductWithInvalidSellByType = () => _productConfigurationService.CreateProduct(createProductDto);

            addProductWithInvalidSellByType.Should().Throw<ArgumentException>().WithMessage(message);
        }

        #endregion

        #region Update product

        [Theory]
        [InlineData(null, "*Product name is required*")]
        [InlineData("", "*Product name is required*")]
        [InlineData(" ", "*Product name is required*")]
        [InlineData("milk", "*Product name \"milk\" does not exist*")]
        public void UpdateProduct_WithInvalidName_ThrowsArgumentException(string productName, string message)
        {
            var updateProductDto = new UpsertProductDto(productName, 1m, "Unit");

            Action addProductWithInvalidName = () => _productConfigurationService.UpdateProduct(updateProductDto);

            addProductWithInvalidName.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData("can of soup", 0.99, "Weight")]
        public void UpdateProduct_UpdatesNonIdentityFieldsInPersistedProduct(string productName, double retailPrice, string sellByType)
        {
            var updateProductDto = new UpsertProductDto(productName, 1m, "Unit");

            var persistedProductDto = _productConfigurationService.UpdateProduct(updateProductDto);

            persistedProductDto.Should().BeEquivalentTo(updateProductDto);
        }

        #endregion
    }
}