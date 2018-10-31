using System;
using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class WeightedLineItemFactoryTest : LineItemFactoryTest
    {
        [Fact]
        public override void CreateLineItem_ReturnsLineItem()
        {
            var weight = 1.5m;
            var item = new WeightedItem(ProductProvider.GetProductSoldByWeight(), weight);
            var lineItem = new WeightedLineItemFactory(item).CreateLineItem();

            lineItem.SalePrice.Should().Be(item.Product.RetailPrice * weight);
        }
    }
}