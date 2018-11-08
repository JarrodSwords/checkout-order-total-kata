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
    public abstract class IProductSpecialConfigurationServiceTest
    {
        private DateTime _now = DateTime.Now;
        protected IProductSpecialConfigurationService _productSpecialConfigurationService;

        [Fact]
        public void CreateBuyNForXAmountSpecial_CreatesSpecial()
        {
            var args = new CreateBuyNForXAmountSpecialArgs
            {
                DiscountedItems = 3,
                EndTime = _now.EndOfWeek(),
                GroupSalePrice = 2m,
                Limit = 6,
                ProductName = "can of soup",
                StartTime = _now.StartOfWeek()
            };

            var productDto = _productSpecialConfigurationService.CreateBuyNForXAmountSpecial(args);
            var specialDto = (BuyNForXAmountSpecialDto) productDto.Special;

            productDto.Name.Should().Be(args.ProductName);
            specialDto.DiscountedItems.Should().Be(args.DiscountedItems);
            specialDto.EndTime.Should().Be(args.EndTime);
            specialDto.GroupSalePrice.Should().Be(args.GroupSalePrice);
            specialDto.Limit.Should().Be(args.Limit);
            specialDto.StartTime.Should().Be(args.StartTime);
        }

        [Theory]
        [InlineData(null, "*Product name is required*")]
        [InlineData("", "*Product name is required*")]
        [InlineData(" ", "*Product name is required*")]
        [InlineData("milk", "*Product name \"milk\" does not exist*")]
        public void CreateBuyNForXAmountSpecial_WithInvalidProductName_ThrowsArgumentException(string productName, string message)
        {
            var args = new CreateBuyNForXAmountSpecialArgs { ProductName = productName };

            Action createSpecial = () => _productSpecialConfigurationService.CreateBuyNForXAmountSpecial(args);
            
            createSpecial.Should().Throw<ArgumentException>(message);
        }

        [Fact]
        public void CreateBuyNGetMAtXPercentOffSpecial_CreatesSpecial()
        {
            var args = new CreateBuyNGetMAtXPercentOffSpecialArgs
            {
                DiscountedItems = 1,
                EndTime = _now.EndOfWeek(),
                Limit = 6,
                PercentageOff = 50m,
                PreDiscountItems = 2,
                ProductName = "can of soup",
                StartTime = _now.StartOfWeek()
            };

            var productDto = _productSpecialConfigurationService.CreateBuyNGetMAtXPercentOffSpecial(args);
            var specialDto = (BuyNGetMAtXPercentOffSpecialDto) productDto.Special;

            productDto.Name.Should().Be(args.ProductName);
            specialDto.DiscountedItems.Should().Be(args.DiscountedItems);
            specialDto.EndTime.Should().Be(args.EndTime);
            specialDto.Limit.Should().Be(args.Limit);
            specialDto.PercentageOff.Should().Be(args.PercentageOff);
            specialDto.PreDiscountItems.Should().Be(args.PreDiscountItems);
            specialDto.StartTime.Should().Be(args.StartTime);
        }

        [Theory]
        [InlineData(null, "*Product name is required*")]
        [InlineData("", "*Product name is required*")]
        [InlineData(" ", "*Product name is required*")]
        [InlineData("milk", "*Product name \"milk\" does not exist*")]
        public void CreateBuyNGetMAtXPercentOffSpecial_WithInvalidProductName_ThrowsArgumentException(string productName, string message)
        {
            var args = new CreateBuyNGetMAtXPercentOffSpecialArgs { ProductName = productName };

            Action createSpecial = () => _productSpecialConfigurationService.CreateBuyNGetMAtXPercentOffSpecial(args);
            
            createSpecial.Should().Throw<ArgumentException>(message);
        }

        [Fact]
        public void CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial_CreatesSpecial()
        {
            var args = new CreateBuyNGetMAtXPercentOffSpecialArgs
            {
                DiscountedItems = 1,
                EndTime = _now.EndOfWeek(),
                Limit = 6,
                PercentageOff = 50m,
                PreDiscountItems = 2,
                ProductName = "can of soup",
                StartTime = _now.StartOfWeek()
            };

            var productDto = _productSpecialConfigurationService.CreateBuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(args);
            var specialDto = (BuyNGetMAtXPercentOffSpecialDto) productDto.Special;

            productDto.Name.Should().Be(args.ProductName);
            specialDto.DiscountedItems.Should().Be(args.DiscountedItems);
            specialDto.EndTime.Should().Be(args.EndTime);
            specialDto.Limit.Should().Be(args.Limit);
            specialDto.PercentageOff.Should().Be(args.PercentageOff);
            specialDto.PreDiscountItems.Should().Be(args.PreDiscountItems);
            specialDto.StartTime.Should().Be(args.StartTime);
        }
    }
}
