using System;
using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.ApplicationServiceImplementations;
using PillarTechnology.GroceryPointOfSale.ApplicationServices;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class BuyNGetMAtXPercentOffConfigurationServiceTest 
    {
        protected DateTime _now = DependencyProvider.CreateDateTimeProvider().Now;
        protected BuyNGetMAtXPercentOffConfigurationService _service = DependencyProvider.CreateBuyNGetMAtXPercentOffConfigurationService();

        [Fact]
        public void CreateBuyNForXAmountSpecial_CreatesSpecial()
        {
            var args = new CreateSpecialArgs
            {
                DiscountedItems = 1,
                EndTime = _now.EndOfWeek(),
                Limit = 6,
                PercentageOff = 50m,
                PreDiscountItems = 2,
                ProductName = "can of soup",
                StartTime = _now.StartOfWeek()
            };

            var productDto = _service.CreateSpecial(args);
            var specialDto = (BuyNGetMAtXPercentOffSpecialDto) productDto.Special;

            productDto.Name.Should().Be(args.ProductName);
            specialDto.DiscountedItems.Should().Be(args.DiscountedItems);
            specialDto.EndTime.Should().Be(args.EndTime.Value);
            specialDto.Limit.Should().Be(args.Limit);
            specialDto.PercentageOff.Should().Be(args.PercentageOff);
            specialDto.PreDiscountItems.Should().Be(args.PreDiscountItems);
            specialDto.StartTime.Should().Be(args.StartTime.Value);
        }

        [Theory]
        [InlineData(null, "*Product name is required*")]
        [InlineData("", "*Product name is required*")]
        [InlineData(" ", "*Product name is required*")]
        [InlineData("milk", "*Product name \"milk\" does not exist*")]
        public void CreateBuyNGetMAtXPercentOffSpecial_WithInvalidProductName_ThrowsArgumentException(string productName, string message)
        {
            var args = new CreateSpecialArgs { ProductName = productName };

            Action createSpecial = () => _service.CreateSpecial(args);

            createSpecial.Should().Throw<ArgumentException>(message);
        }
    }
}
