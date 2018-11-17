using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using PointOfSale.Services;
using PointOfSale.Domain;
using Xunit;

namespace PointOfSale.Test
{
    public abstract class IProductConfigurationServiceTest
    {
        protected readonly DateTime _now = DependencyProvider.CreateDateTimeProvider().Now;
        protected IProductConfigurationService _productConfigurationService;
        protected IProductService _productService;

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
    }
}