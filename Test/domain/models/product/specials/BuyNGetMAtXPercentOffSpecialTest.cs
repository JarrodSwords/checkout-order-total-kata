using System.Linq;
using FluentAssertions;
using NodaMoney;
using PointOfSale.Domain;
using Xunit;

namespace PointOfSale.Test.Domain
{
    public class BuyNGetMAtXPercentOffSpecialTest : SpecialTest
    {
        public Special CreateSpecial(int preDiscountItems, int discountedItems, decimal percentOff, int? limit)
        {
            return new BuyNGetMAtXPercentOffSpecial.Factory(DependencyProvider.CreateDateTimeProvider())
                .Configure(discountedItems, _now.EndOfWeek(), percentOff, preDiscountItems, _now.StartOfWeek(), limit)
                .CreateSpecial();
        }

        [Theory]
        [InlineData(1, 1, 0, 0)]
        [InlineData(1, 1, 1, 0)]
        [InlineData(1, 1, 2, 1)]
        [InlineData(1, 1, 3, 1)]
        [InlineData(1, 1, 4, 2)]
        [InlineData(1, 1, 4, 1, 2)]
        [InlineData(2, 1, 1, 0)]
        [InlineData(2, 1, 3, 1)]
        [InlineData(2, 1, 4, 1)]
        [InlineData(2, 2, 4, 1)]
        [InlineData(2, 2, 12, 2, 8)]
        public void CreateLineItems_CreatesCorrectLineItemCount(
            int preDiscountItems,
            int discountedItems,
            int scannedItemCount,
            int expectedLineItemCount,
            int? limit = null
        )
        {
            var product = new EachesProduct("test product", 1m);
            product.Special = CreateSpecial(preDiscountItems, discountedItems, 100, limit);

            CreateLineItems(product, scannedItemCount);

            _lineItems.Count().Should().Be(expectedLineItemCount);
        }

        [Theory]
        [InlineData(1, 1, 1, 100, 2, -1)]
        [InlineData(2, 1, 1, 100, 2, -2)]
        [InlineData(2, 1, 1, 100, 3, -2)]
        [InlineData(2, 1, 1, 100, 4, -4)]
        [InlineData(2, 1, 1, 100, 5, -4)]
        [InlineData(1, 2, 1, 50, 3, -0.5)]
        [InlineData(2, 2, 1, 50, 3, -1)]
        [InlineData(2, 2, 1, 50, 6, -2)]
        [InlineData(2, 2, 2, 50, 4, -2)]
        public virtual void CreateLineItems_CreatesCorrectLineItemTotalValue(
            double retailPrice,
            int preDiscountItems,
            int discountedItems,
            double percentageOff,
            int scannedItemCount,
            double expectedTotalValue
        )
        {
            var product = new EachesProduct("test product", (decimal) retailPrice);
            product.Special = CreateSpecial(preDiscountItems, discountedItems, (decimal) percentageOff, null);

            CreateLineItems(product, scannedItemCount);

            var totalValue = Money.USDollar(_lineItems.Sum(x => x.SalePrice.Amount));
            totalValue.Should().BeEquivalentTo(Money.USDollar(expectedTotalValue));
        }

        [Theory]
        [InlineData(1, 0.25, 2, 1, 50, 3)]
        [InlineData(2, 0.5, 3, 1, 100, 5)]
        public void CreateLineItems_WithBetterMarkdown_CreatesZeroLineItems(
            double retailPrice,
            double markdown,
            int preDiscountItems,
            int discountedItems,
            double percentageOff,
            int scannedItemCount
        )
        {
            var product = new EachesProduct("test product", (decimal) retailPrice)
            {
                Markdown = MarkdownProvider.GetMarkdown(DateRange.Active, (decimal) markdown)
            };

            product.Special = CreateSpecial(preDiscountItems, discountedItems, (decimal) percentageOff, null);

            CreateLineItems(product, scannedItemCount);

            _lineItems.Count().Should().Be(0);
        }
    }
}
