using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using PointOfSale.Domain;
using PointOfSale.Services;
using PointOfSale.Test.Domain;
using Xunit;

namespace PointOfSale.Test.Services
{
    public abstract class IProductConfigurationServiceTest
    {
        protected readonly DateTime _now = DependencyProvider.CreateDateTimeProvider().Now;
        protected IProductConfigurationService _productConfigurationService;
        protected IProductService _productService;

        [Theory]
        [InlineData("milk", 1.99, null, "eaches")]
        [InlineData("turkey", null, 1.5, "mass")]
        public void CreateProduct_CreatesPersistedProduct(
            string productName,
            double? retailPrice,
            double? retailPriceByUnit,
            string sellByType,
            double? massAmount = null,
            string massUnit = ""
        )
        {
            var args = sellByType == "eaches" ?
                new UpsertProductArgs(productName, (decimal) retailPrice, sellByType) :
                new UpsertProductArgs(massAmount, massUnit, productName, (decimal) retailPriceByUnit, sellByType);

            var persistedProductDto = _productConfigurationService.CreateProduct(args);

            persistedProductDto.Name.Should().Be(args.ProductName);
            // persistedProductDto.RetailPrice.Should().Be(args.RetailPrice);
            persistedProductDto.SellByType.Should().Be(args.SellByType);
        }

        [Theory]
        [InlineData("can of soup", 0.99, null, "eaches")]
        [InlineData("lean ground beef", null, 2.5, "mass")]
        public void UpdateProduct_UpdatesNonIdentityFieldsInPersistedProduct(
            string productName,
            double? retailPrice,
            double? retailPriceByUnit,
            string sellByType,
            double? massAmount = null,
            string massUnit = ""
        )
        {
            var args = sellByType == "eaches" ?
                new UpsertProductArgs(productName, (decimal) retailPrice, sellByType) :
                new UpsertProductArgs(massAmount, massUnit, productName, (decimal) retailPriceByUnit, sellByType);

            var persistedProductDto = _productConfigurationService.UpdateProduct(args);

            persistedProductDto.Name.Should().Be(args.ProductName);
            // persistedProductDto.RetailPrice.Should().Be(args.RetailPrice);
            persistedProductDto.SellByType.Should().Be(args.SellByType);
        }
    }
}
