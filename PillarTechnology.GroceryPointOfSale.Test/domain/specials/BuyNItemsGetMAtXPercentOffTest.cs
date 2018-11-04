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

        private IEnumerable<ScannedItem> CreateScannedItems(Product product, int count)
        {
            for (var i = 0; i < count; i++)
                yield return new ScannedItem(product) { Id = i + 1 };
        }

        [Theory]
        [InlineData(1, 1, 1, 0)]
        [InlineData(1, 1, 2, 1)]
        [InlineData(1, 1, 3, 1)]
        [InlineData(1, 1, 4, 2)]
        [InlineData(2, 1, 1, 0)]
        [InlineData(2, 1, 2, 0)]
        [InlineData(2, 1, 3, 1)]
        [InlineData(2, 1, 4, 1)]
        public void CreateLineItems_CreatesCorrectLineItemCount(int preDiscountItems, int discountedItems, int scannedItemCount, int validSpecialCount)
        {
            var product = new Product("test product", Money.USDollar(1m), SellByType.Unit);
            var special = new BuyNGetMAtXPercentOffSpecial(_now.StartOfWeek(), _now.EndOfWeek(), preDiscountItems, discountedItems, 100);
            var scannedItems = CreateScannedItems(product, scannedItemCount);
            var productSpecial = new ProductSpecial(product, special);

            var lineItems = productSpecial.CreateLineItems(scannedItems);

            lineItems.Count().Should().Be(validSpecialCount);
        }
    }
}
