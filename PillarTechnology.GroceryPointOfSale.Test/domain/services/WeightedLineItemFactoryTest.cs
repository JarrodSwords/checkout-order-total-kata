using System;
using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class WeightedLineItemFactoryTest : LineItemFactoryTest
    {
        private ProductTestData _productTestData = new ProductTestData();

        [Fact]
        public override void CreateLineItem_ReturnsLineItem()
        {
            var weight = 1.5m;
            var item = new WeightedItem(_productTestData.GetProductSoldByWeight(), weight);
            var lineItem = new WeightedLineItemFactory(item).CreateLineItem();

            lineItem.SalePrice.Should().Be(item.Product.RetailPrice * weight);
        }
    }
}