using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class ProductConfigurationServiceTest : IProductConfigurationServiceTest
    {
        public ProductConfigurationServiceTest()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>()));
            var productRepository = new InMemoryProductRepositoryFactory().CreateSeededRepository();
            var createProductArgsValidator = new CreateProductArgsValidator(productRepository);
            var updateProductArgsValidator = new UpdateProductArgsValidator(productRepository);
            var upsertProductMarkdownArgsValidator = new UpsertProductMarkdownArgsValidator(productRepository);

            _productConfigurationService = new ProductConfigurationService(mapper, productRepository, createProductArgsValidator, updateProductArgsValidator, upsertProductMarkdownArgsValidator);
            _productService = new ProductService(mapper, productRepository);
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

        #region Upsert product markdown

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

        #region Test data

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

        #endregion Upsert product
    }
}