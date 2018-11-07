using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NodaMoney;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class BuyNGetMAtXPercentOffSpecialTest
    {
        private DateTime _now = DateTime.Now;
        private IEnumerable<LineItem> _lineItems;

        private IEnumerable<ScannedItem> CreateScannedItems(Product product, int count)
        {
            for (var i = 0; i < count; i++)
                yield return new ScannedItem(product) { Id = i + 1 };
        }

        private void CreateLineItems(Product product, Special special, int scannedItemCount)
        {
            var scannedItems = CreateScannedItems(product, scannedItemCount);
            var productSpecial = new ProductSpecial(product, special);

            _lineItems = productSpecial.CreateLineItems(scannedItems);
        }

        [Theory]
        [InlineData(1, 1, 0, 0)]
        [InlineData(1, 1, 1, 0)]
        [InlineData(1, 1, 2, 1)]
        [InlineData(1, 1, 3, 1)]
        [InlineData(1, 1, 4, 2)]
        [InlineData(2, 1, 1, 0)]
        [InlineData(2, 1, 3, 1)]
        [InlineData(2, 1, 4, 1)]
        [InlineData(2, 2, 4, 1)]
        public void CreateLineItems_CreatesCorrectLineItemCount(int preDiscountItems, int discountedItems, int scannedItemCount, int expectedLineItemCount)
        {
            var product = new Product("test product", Money.USDollar(1m), SellByType.Unit);
            var special = new BuyNGetMAtXPercentOffSpecial(_now.StartOfWeek(), _now.EndOfWeek(), preDiscountItems, discountedItems, 100);

            CreateLineItems(product, special, scannedItemCount);

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
        public void CreateLineItems_CreatesCorrectLineItemTotalValue(double retailPrice, int preDiscountItems, int discountedItems, double percentageOff, int scannedItemCount, double expectedTotalValue)
        {
            var product = new Product("test product", Money.USDollar(retailPrice), SellByType.Unit);
            var special = new BuyNGetMAtXPercentOffSpecial(_now.StartOfWeek(), _now.EndOfWeek(), preDiscountItems, discountedItems, (decimal) percentageOff);

            CreateLineItems(product, special, scannedItemCount);

            var totalValue = Money.USDollar(_lineItems.Sum(x => x.SalePrice.Amount));
            totalValue.Should().BeEquivalentTo(Money.USDollar(expectedTotalValue));
        }

        [Theory]
        [InlineData(1, 0.25, 2, 1, 50, 3)]
        [InlineData(2, 0.5, 3, 1, 100, 5)]
        public void CreateLineItems_WithBetterMarkdown_CreatesZeroLineItems(double retailPrice, double markdown, int preDiscountItems, int discountedItems, double percentageOff, int scannedItemCount)
        {
            var product = new Product("test product", Money.USDollar(retailPrice), SellByType.Unit)
            {
                Markdown = MarkdownProvider.GetMarkdown(DateRange.Active, (decimal)markdown)
            };

            var special = new BuyNGetMAtXPercentOffSpecial(_now.StartOfWeek(), _now.EndOfWeek(), preDiscountItems, discountedItems, (decimal)percentageOff);

            CreateLineItems(product, special, scannedItemCount);

            _lineItems.Count().Should().Be(0);
        }
    }
}
