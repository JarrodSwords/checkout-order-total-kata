using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NodaMoney;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialTest : SpecialTest
    {
        protected override IEnumerable<ScannedItem> CreateScannedItems(Product product, int count)
        {
            var weight = 0m;

            for (var i = 0; i < count; i++)
            {
                weight += 0.5m;
                yield return new WeightedScannedItem(product, weight) { Id = i + 1 };
            }
        }

        public Special CreateSpecial(int preDiscountItems, int discountedItems, decimal percentOff, int? limit)
        {
            return new BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(_now.StartOfWeek(), _now.EndOfWeek(), preDiscountItems, discountedItems, percentOff, limit);
        }

        [Theory]
        [InlineData(1, 1, 1, 100, 2, -0.5)]
        [InlineData(1, 2, 1, 50, 3, -0.25)]
        [InlineData(1, 3, 2, 50, 5, -0.75)]
        public void CreateLineItems_CreatesCorrectLineItemTotalValue(double retailPrice, int preDiscountItems, int discountedItems, double percentageOff, int scannedItemCount, double expectedTotalValue)
        {
            var product = new Product("test product", Money.USDollar(retailPrice), SellByType.Unit);
            var special = CreateSpecial(preDiscountItems, discountedItems, (decimal) percentageOff, null);

            CreateLineItems(product, special, scannedItemCount);

            var totalValue = Money.USDollar(_lineItems.Sum(x => x.SalePrice.Amount));
            totalValue.Should().BeEquivalentTo(Money.USDollar(expectedTotalValue));
        }
    }
}
