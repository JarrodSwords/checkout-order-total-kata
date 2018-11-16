using System;
using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class ProductConfigurationServiceTest : IProductConfigurationServiceTest
    {
        public ProductConfigurationServiceTest()
        {
            _productConfigurationService = DependencyProvider.CreateProductConfigurationService();
            _productService = DependencyProvider.CreateProductService();
        }

        #region Create product

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

        #endregion Create product

        #region Update product

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

        #endregion Update product
    }
}