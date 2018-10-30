using System;
using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class EachesLineItemFactoryTest : LineItemFactoryTest
    {
        private ProductTestData _productTestData = new ProductTestData();

        [Fact]
        public override void CreateLineItem_ReturnsLineItem()
        {
            var item = new Item(_productTestData.GetProductSoldByUnit());
            var lineItem = new EachesLineItemFactory(item).CreateLineItem();

            lineItem.SalePrice.Should().Be(item.Product.RetailPrice);
        }
    }
}