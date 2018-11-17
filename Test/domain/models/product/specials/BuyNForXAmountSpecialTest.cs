using System.Linq;
using FluentAssertions;
using NodaMoney;
using PointOfSale.Domain;
using Xunit;

namespace PointOfSale.Test.Domain
{
    public class BuyNForXAmountTest : SpecialTest
    {
        private BuyNForXAmountSpecial.Factory _factory = new BuyNForXAmountSpecial.Factory(new BasicDateTimeProvider());

        [Theory]
        [InlineData(3, 0, 0)]
        [InlineData(3, 3, 1)]
        [InlineData(3, 7, 2)]
        [InlineData(3, 7, 1, 3)]
        public void CreateLineItems_CreatesCorrectLineItemCount(int discountedItems, int scannedItemCount, int expectedLineItemCount, int? limit = null)
        {
            var product = new Product("test product", Money.USDollar(1m), SellByType.Unit);

            var special = _factory
                .Configure(discountedItems, _now.EndOfWeek(), Money.USDollar(1.5m), _now.StartOfWeek(), limit)
                .CreateSpecial();

            CreateLineItems(product, special, scannedItemCount);

            _lineItems.Count().Should().Be(expectedLineItemCount);
        }

        [Theory]
        [InlineData(3, 1, 3, -2)]
        [InlineData(3, 1.5, 6, -3)]
        [InlineData(3, 2, 7, -2)]
        public void CreateLineItems_CreatesCorrectLineItemTotalValue(int discountedItems, double groupSalePrice, int scannedItemCount, int expectedTotalValue)
        {
            var product = new Product("test product", Money.USDollar(1m), SellByType.Unit);

            var special = _factory
                .Configure(discountedItems, _now.EndOfWeek(), Money.USDollar((decimal) groupSalePrice), _now.StartOfWeek())
                .CreateSpecial();

            CreateLineItems(product, special, scannedItemCount);

            var totalValue = Money.USDollar(_lineItems.Sum(x => x.SalePrice.Amount));
            totalValue.Should().BeEquivalentTo(Money.USDollar(expectedTotalValue));
        }

        [Theory]
        [InlineData(1, 0.2, 3, 2.5, 3)]
        [InlineData(2, 0.5, 2, 3.5, 3)]
        [InlineData(2, 0.5, 2, 3.5, 6)]
        public void CreateLineItems_WithBetterMarkdown_CreatesZeroLineItems(double retailPrice, double markdown, int discountedItems, double groupSalePrice, int scannedItemCount)
        {
            var product = new Product("test product", Money.USDollar(retailPrice), SellByType.Unit)
            {
                Markdown = MarkdownProvider.GetMarkdown(DateRange.Active, (decimal) markdown)
            };

            var special = _factory
                .Configure(discountedItems, _now.EndOfWeek(), Money.USDollar((decimal) groupSalePrice), _now.StartOfWeek())
                .CreateSpecial();

            CreateLineItems(product, special, scannedItemCount);

            _lineItems.Count().Should().Be(0);
        }

        [Theory]
        [InlineData(1, 3, 3.5, 3)]
        [InlineData(2, 2, 5, 4)]
        public void CreateLineItems_WithBetterRetailPrice_CreatesZeroLineItems(double retailPrice, int discountedItems, double groupSalePrice, int scannedItemCount)
        {
            var product = new Product("test product", Money.USDollar(retailPrice), SellByType.Unit);

            var special = _factory
                .Configure(discountedItems, _now.EndOfWeek(), Money.USDollar((decimal) groupSalePrice), _now.StartOfWeek())
                .CreateSpecial();

            CreateLineItems(product, special, scannedItemCount);

            _lineItems.Count().Should().Be(0);
        }
    }
}
