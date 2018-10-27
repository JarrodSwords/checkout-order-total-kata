using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Moq;
using NodaMoney;
using PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public abstract class IProductConfigurationServiceTest
    {
        protected IProductConfigurationService _productConfigurationService;
        protected IProductService _productService;

        [Fact]
        public void CreateProduct_CreatesPersistedProduct()
        {
            var productDto = new ProductDto
            {
                Name = "milk",
                RetailPrice = 1.99m
            };

            var persistedProductDto = _productConfigurationService.CreateProduct(productDto);
            persistedProductDto.Should().BeEquivalentTo(productDto);
        }

        [Theory]
        [ClassData(typeof(ProductTestData))]
        public void CreateProduct_WhenProductExists_ThrowsArgumentException(string productName)
        {
            var productDto = _productService.FindProduct(productName);

            Action addExistingProduct = () => _productConfigurationService.CreateProduct(productDto);

            addExistingProduct.Should().Throw<ArgumentException>()
                .WithMessage("Product already exists");
        }

        [Theory]
        [ClassData(typeof(ProductTestData))]
        public void UpdateProduct_SetRetailPrice_UpdatesNonIdentityFieldsInPersistedProduct(string productName)
        {
            var productDto = _productService.FindProduct(productName);
            productDto.RetailPrice++;

            var persistedProductDto = _productConfigurationService.UpdateProduct(productDto);

            persistedProductDto.Should().BeEquivalentTo(productDto);
        }
    }
}