using System;
using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public abstract class IProductSpecialConfigurationServiceTest
    {
        protected DateTime _now = DateTime.Now;
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
            specialDto.EndTime.Should().Be(args.EndTime.Value);
            specialDto.GroupSalePrice.Should().Be(args.GroupSalePrice);
            specialDto.Limit.Should().Be(args.Limit);
            specialDto.StartTime.Should().Be(args.StartTime.Value);
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
            specialDto.EndTime.Should().Be(args.EndTime.Value);
            specialDto.Limit.Should().Be(args.Limit);
            specialDto.PercentageOff.Should().Be(args.PercentageOff);
            specialDto.PreDiscountItems.Should().Be(args.PreDiscountItems);
            specialDto.StartTime.Should().Be(args.StartTime.Value);
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
            specialDto.EndTime.Should().Be(args.EndTime.Value);
            specialDto.Limit.Should().Be(args.Limit);
            specialDto.PercentageOff.Should().Be(args.PercentageOff);
            specialDto.PreDiscountItems.Should().Be(args.PreDiscountItems);
            specialDto.StartTime.Should().Be(args.StartTime.Value);
        }
    }
}
