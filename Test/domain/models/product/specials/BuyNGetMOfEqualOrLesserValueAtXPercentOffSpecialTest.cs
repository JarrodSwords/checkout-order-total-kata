using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NodaMoney;
using PointOfSale.Domain;
using UnitsNet;
using UnitsNet.Units;
using Xunit;

namespace PointOfSale.Test.Domain
{
    public class BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecialTest : SpecialTest
    {
        protected override IEnumerable<ScannedItem> CreateScannedItems(Product product, int count)
        {
            double weight = 1;

            for (var i = 0; i < count; i++)
                yield return new MassScannedItem(weight, "Pound", (MassProduct) product) { Id = i + 1 };
        }

        public Special CreateSpecial(int preDiscountItems, int discountedItems, decimal percentOff, int? limit) =>
            new BuyNGetMOfEqualOrLesserValueAtXPercentOffSpecial(
                discountedItems,
                _now.EndOfWeek(),
                percentOff,
                preDiscountItems,
                _now.StartOfWeek(),
                limit
            );

        [Theory]
        [InlineData(1, 1, 1, 100, 2, -1)]
        [InlineData(1, 2, 1, 50, 3, -0.50)]
        [InlineData(1, 3, 2, 50, 5, -1)]
        public void CreateLineItems_CreatesCorrectLineItemTotalValue(
            double retailPrice,
            int preDiscountItems,
            int discountedItems,
            double percentageOff,
            int scannedItemCount,
            double expectedTotalValue
        )
        {
            var product = new MassProduct("test product", (decimal) retailPrice);
            product.Special = CreateSpecial(preDiscountItems, discountedItems, (decimal) percentageOff, null);

            CreateLineItems(product, scannedItemCount);

            var totalValue = Money.USDollar(_lineItems.Sum(x => x.SalePrice.Amount));
            totalValue.Should().BeEquivalentTo(Money.USDollar(expectedTotalValue));
        }
    }
}
