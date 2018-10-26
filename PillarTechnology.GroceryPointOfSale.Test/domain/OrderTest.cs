using FluentAssertions;
using Moq;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class OrderTest
    {
        [Fact]
        public void AddPurchasable_PurchasableIsAddedToScannedItems()
        {
            var dummyPurchasable = new Mock<IPurchasable>().Object;
            var order = new Order();

            order.AddPurchasable(dummyPurchasable);

            order.ScannedItems.Should().Contain(dummyPurchasable);
        }
    }
}