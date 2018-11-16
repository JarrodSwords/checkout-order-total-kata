using System;
using FluentAssertions;
using PointOfSale.ApplicationServiceImplementations;
using PointOfSale.ApplicationServices;
using PointOfSale.Domain;
using Xunit;

namespace PointOfSale.Test
{
    public class BuyNForXAmountConfigurationServiceTest 
    {
        protected DateTime _now = DependencyProvider.CreateDateTimeProvider().Now;
        protected BuyNForXAmountConfigurationService _service = DependencyProvider.CreateBuyNForXAmountConfigurationService();

        [Fact]
        public void CreateBuyNForXAmountSpecial_CreatesSpecial()
        {
            var args = new CreateSpecialArgs
            {
                DiscountedItems = 3,
                EndTime = _now.EndOfWeek(),
                GroupSalePrice = 2m,
                Limit = 6,
                ProductName = "can of soup",
                StartTime = _now.StartOfWeek()
            };

            var productDto = _service.CreateSpecial(args);
            var specialDto = (BuyNForXAmountSpecialDto) productDto.Special;

            productDto.Name.Should().Be(args.ProductName);
            specialDto.DiscountedItems.Should().Be(args.DiscountedItems);
            specialDto.EndTime.Should().Be(args.EndTime.Value);
            specialDto.GroupSalePrice.Should().Be(args.GroupSalePrice);
            specialDto.Limit.Should().Be(args.Limit);
            specialDto.StartTime.Should().Be(args.StartTime.Value);
        }

        [Theory]
        [InlineData(null, "*Product name is required*")]
        [InlineData("", "*Product name is required*")]
        [InlineData(" ", "*Product name is required*")]
        [InlineData("milk", "*Product name \"milk\" does not exist*")]
        public void CreateBuyNForXAmountSpecial_WithInvalidProductName_ThrowsArgumentException(string productName, string message)
        {
            var args = new CreateSpecialArgs { ProductName = productName };

            Action createSpecial = () => _service.CreateSpecial(args);

            createSpecial.Should().Throw<ArgumentException>(message);
        }
    }
}
