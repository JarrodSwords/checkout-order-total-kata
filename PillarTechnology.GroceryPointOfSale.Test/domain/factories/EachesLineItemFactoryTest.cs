using System;
using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class EachesLineItemFactoryTest : LineItemFactoryTest
    {
        [Fact]
        public override void CreateLineItem_ReturnsLineItem()
        {
            var item = new Item(ProductProvider.GetProductSoldByUnit());
            var lineItem = new EachesLineItemFactory(item).CreateLineItem();

            lineItem.SalePrice.Should().Be(item.Product.RetailPrice);
        }
    }
}