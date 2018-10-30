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

        [Fact]
        public void CreateProduct_WithExistingProduct_ThrowsArgumentException()
        {
            var productDto = new ProductTestData().GetProductSoldByUnit();
            var createProductDto = new CreateProductDto { Name = productDto.Name };

            Action addExistingProduct = () => _productConfigurationService.CreateProduct(createProductDto);

            addExistingProduct.Should().Throw<ArgumentException>()
                .WithMessage($"*Product name \"{productDto.Name}\" already exists*");
        }

        [Fact]
        public void CreateProduct_WithNegativeRetailPrice_ThrowsArgumentException()
        {
            var productDto = new CreateProductDto("milk", -1m, "Unit");

            Action addIncompleteProduct = () => _productConfigurationService.CreateProduct(productDto);

            addIncompleteProduct.Should().Throw<ArgumentException>()
                .WithMessage("*Product retail price cannot be negative*");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CreateProduct_WithoutName_ThrowsArgumentException(string productName)
        {
            var createProductDto = new CreateProductDto(productName, 1m, "Unit");

            Action addIncompleteProduct = () => _productConfigurationService.CreateProduct(createProductDto);

            addIncompleteProduct.Should().Throw<ArgumentException>()
                .WithMessage("*Product name is required*");
        }

        [Fact]
        public void CreateProduct_WithoutRetailPrice_ThrowsArgumentException()
        {
            var productDto = new CreateProductDto("milk", null, "Unit");

            Action addIncompleteProduct = () => _productConfigurationService.CreateProduct(productDto);

            addIncompleteProduct.Should().Throw<ArgumentException>()
                .WithMessage($"*Product retail price is required*");
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

        private CreateProductDto CreateCreateProductDto(string productName, double? retailPrice)
        {
            return new CreateProductDto
            {
                Name = productName,
                    RetailPrice = (decimal?) retailPrice
            };
        }
    }
}