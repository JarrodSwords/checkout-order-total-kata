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
        public void UpsertProductMarkdown_UpsertsProductMarkdown()
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
    }
}
