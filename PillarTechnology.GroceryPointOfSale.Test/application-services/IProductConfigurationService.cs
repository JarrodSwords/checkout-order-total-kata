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
            var createProductDto = new CreateProductDto(productName, (decimal) retailPrice, sellByType);

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
            var createProductDto = new CreateProductDto(productName, 1m, "Unit");

            Action addIncompleteProduct = () => _productConfigurationService.CreateProduct(createProductDto);

            addIncompleteProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*Product retail price is required*")]
        [InlineData(-1, "*Product retail price cannot be negative*")]
        public void CreateProduct_WithInvalidRetailPrice_ThrowsArgumentException(double? retailPrice, string message)
        {
            var createProductDto = new CreateProductDto("milk", (decimal?)retailPrice, "Unit");

            Action addIncompleteProduct = () => _productConfigurationService.CreateProduct(createProductDto);

            addIncompleteProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*Product sell by type is required*")]
        [InlineData("", "*Product sell by type is required*")]
        [InlineData(" ", "*Product sell by type is required*")]
        [InlineData("Volume", "*Product sell by type \"Volume\" is not in: Unit, Weight*")]
        public void CreateProduct_WithInvalidSellByType_ThrowsArgumentException(string sellByType, string message)
        {
            var createProductDto = new CreateProductDto("milk", 1.99m, sellByType);

            Action addIncompleteProduct = () => _productConfigurationService.CreateProduct(createProductDto);

            addIncompleteProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        #endregion

        #region Update product

        [Theory]
        [ClassData(typeof(ProductTestData))]
        public void UpdateProduct_SetRetailPrice_UpdatesNonIdentityFieldsInPersistedProduct(string productName)
        {
            var productDto = _productService.FindProduct(productName);
            productDto.RetailPrice++;

            var persistedProductDto = _productConfigurationService.UpdateProduct(productDto);

            persistedProductDto.Should().BeEquivalentTo(productDto);
        }

        #endregion
    }
}