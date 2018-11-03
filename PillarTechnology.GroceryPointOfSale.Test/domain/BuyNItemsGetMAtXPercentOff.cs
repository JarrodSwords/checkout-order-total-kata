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
    public class BuyNItemsGetMAtXPercentOffTest
    {
        [Theory]
        [ClassData(typeof(BuyNItemsGetMAtXPercentOffTestData))]
        public void ApplySpecial_ForBuyNItemsGetMAtXPercentOff_CreatesCorrectSpecialLineItems(Product product, IEnumerable<ScannedItem> scannedItems, int specialLineItemsCount)
        {
            var lineItems = product.Special.CreateSpecialLineItems(scannedItems);

            lineItems.Count().Should().Be(specialLineItemsCount);
        }

        public class BuyNItemsGetMAtXPercentOffTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                var product = new Product("a product", Money.USDollar(2m), SellByType.Unit);
                var now = DateTime.Now;
                int preDiscountItems = 3, discountedItems = 2, itemsPerSpecial = preDiscountItems + discountedItems;
                var multiplier = 0.5m;

                var special = new BuyNItemsGetMAtXPercentOff(product, now.StartOfWeek(), now.EndOfWeek(), preDiscountItems, discountedItems, multiplier);
                product.Special = special;
                var scannedItem = new ScannedItem(product);

                for (var i = -1; i <= 1; i++)
                    for (var j = 1; j < 3; j++)
                    {
                        var itemCount = (itemsPerSpecial * j) + i;
                        yield return new object[] { product, Enumerable.Repeat(scannedItem, itemCount), itemCount / itemsPerSpecial};
                    }

            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}