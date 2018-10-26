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
            var stubPurchasable = new Mock<IPurchasable>().Object;
            var order = new Order();

            order.AddPurchasable(stubPurchasable);

            order.ScannedItems.Should().Contain(stubPurchasable);
        }
    }
}