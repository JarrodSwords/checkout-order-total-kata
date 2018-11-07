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
    public class BuyNForXAmountTest
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
        [InlineData(3, 0, 0)]
        [InlineData(3, 3, 1)]
        [InlineData(3, 7, 2)]
        public void CreateLineItems_CreatesCorrectLineItemCount(int discountedItems, int scannedItemCount, int expectedLineItemCount)
        {
            var product = new Product("test product", Money.USDollar(1m), SellByType.Unit);
            var special = new BuyNForXAmountSpecial(_now.StartOfWeek(), _now.EndOfWeek(), discountedItems, Money.USDollar(1.5m));

            CreateLineItems(product, special, scannedItemCount);

            _lineItems.Count().Should().Be(expectedLineItemCount);
        }

        [Theory]
        [InlineData(3, 1, 3, -2)]
        [InlineData(3, 1.5, 6, -3)]
        [InlineData(3, 2, 7, -2)]
        public void CreateLineItems_CreatesCorrectLineItemTotalValue(int discountedItems, double salePrice, int scannedItemCount, int expectedTotalValue)
        {
            var product = new Product("test product", Money.USDollar(1m), SellByType.Unit);
            var special = new BuyNForXAmountSpecial(_now.StartOfWeek(), _now.EndOfWeek(), discountedItems, Money.USDollar((decimal)salePrice));

            CreateLineItems(product, special, scannedItemCount);

            var totalValue = Money.USDollar(_lineItems.Sum(x => x.SalePrice.Amount));
            totalValue.Should().BeEquivalentTo(Money.USDollar(expectedTotalValue));
        }
    }
}
