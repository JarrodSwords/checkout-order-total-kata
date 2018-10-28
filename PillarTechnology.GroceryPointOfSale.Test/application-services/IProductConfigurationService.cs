using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentAssertions;using NodaMoney;
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
        [InlineData(null, 1)]
        [InlineData("", 1)]
        [InlineData(" ", 1)]
        public void CreateProduct_WithoutName_ThrowsArgumentException(string productName, double? retailPrice)
        {
            var productDto = CreateProductDto(productName, retailPrice);

            Action addIncompleteProduct = () => _productConfigurationService.CreateProduct(productDto);

            addIncompleteProduct.Should().Throw<ArgumentException>()
                .WithMessage("Product name is required");
        }

        [Theory]
        [InlineData("milk", null)]
        public void CreateProduct_WithoutRetailPrice_ThrowsArgumentException(string productName, double? retailPrice)
        {
            var productDto = CreateProductDto(productName, retailPrice);

            Action addIncompleteProduct = () => _productConfigurationService.CreateProduct(productDto);

            addIncompleteProduct.Should().Throw<ArgumentException>()
                .WithMessage("Product retail price is required");
        }

        [Theory]
        [InlineData("milk", -1)]
        public void CreateProduct_WithNegativeRetailPrice_ThrowsArgumentException(string productName, double? retailPrice)
        {
            var productDto = CreateProductDto(productName, retailPrice);

            Action addIncompleteProduct = () => _productConfigurationService.CreateProduct(productDto);

            addIncompleteProduct.Should().Throw<ArgumentException>()
                .WithMessage("Product retail price must not be negative");
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

        private ProductDto CreateProductDto(string productName, double? retailPrice)
        {
            return new ProductDto
            {
                Name = productName,
                    RetailPrice = (decimal?) retailPrice
            };
        }
    }
}