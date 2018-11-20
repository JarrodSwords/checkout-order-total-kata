using System;
using FluentAssertions;
using PointOfSale.Services;
using PointOfSale.Test.Services;
using Xunit;

namespace PointOfSale.Test.Implementations.Basic
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
        [InlineData(null, "*'Product Name' should not be empty*")]
        [InlineData("", "*'Product Name' should not be empty*")]
        [InlineData(" ", "*'Product Name' should not be empty*")]
        [InlineData("can of soup", "*'Product Name' \"can of soup\" already exists*")]
        public void CreateProduct_WithInvalidName_ThrowsArgumentException(string productName, string message)
        {
            Action createProduct = () => _productConfigurationService.CreateProduct(new UpsertProductArgs(productName, 1m, "eaches"));

            createProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*'Retail Price' must not be empty*")]
        [InlineData(-1, "*'Retail Price' must be greater than or equal to '0'*")]
        public void CreateProduct_WithInvalidRetailPrice_ThrowsArgumentException(double? retailPrice, string message)
        {
            Action createProduct = () => _productConfigurationService.CreateProduct(new UpsertProductArgs("milk", (decimal?) retailPrice, "eaches"));

            createProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*'Sell By Type' should not be empty*")]
        [InlineData("", "*'Sell By Type' should not be empty*")]
        [InlineData(" ", "*'Sell By Type' should not be empty*")]
        [InlineData("volume", "*'Sell By Type' \"volume\" is not in: eaches, mass*")]
        public void CreateProduct_WithInvalidSellByType_ThrowsArgumentException(string sellByType, string message)
        {
            Action createProduct = () => _productConfigurationService.CreateProduct(new UpsertProductArgs("milk", 1.99m, sellByType));

            createProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        #endregion Create product

        #region Update product

        [Theory]
        [InlineData(null, "*'Product Name' should not be empty*")]
        [InlineData("", "*'Product Name' should not be empty*")]
        [InlineData(" ", "*'Product Name' should not be empty*")]
        [InlineData("milk", "*'Product Name' \"milk\" does not exist*")]
        public void UpdateProduct_WithInvalidName_ThrowsArgumentException(string productName, string message)
        {
            Action updateProduct = () => _productConfigurationService.UpdateProduct(new UpsertProductArgs(productName, 1m, "eaches"));

            updateProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*'Retail Price' must not be empty*")]
        [InlineData(-1, "*'Retail Price' must be greater than or equal to '0'*")]
        public void UpdateProduct_WithInvalidRetailPrice_ThrowsArgumentException(double? retailPrice, string message)
        {
            Action updateProduct = () => _productConfigurationService.UpdateProduct(new UpsertProductArgs("milk", (decimal?) retailPrice, "eaches"));

            updateProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        [Theory]
        [InlineData(null, "*'Sell By Type' should not be empty*")]
        [InlineData("", "*'Sell By Type' should not be empty*")]
        [InlineData(" ", "*'Sell By Type' should not be empty*")]
        [InlineData("volume", "*'Sell By Type' \"volume\" is not in: eaches, mass*")]
        public void UpdateProduct_WithInvalidSellByType_ThrowsArgumentException(string sellByType, string message)
        {
            Action updateProduct = () => _productConfigurationService.UpdateProduct(new UpsertProductArgs("milk", 1.99m, sellByType));

            updateProduct.Should().Throw<ArgumentException>().WithMessage(message);
        }

        #endregion Update product
    }
}
