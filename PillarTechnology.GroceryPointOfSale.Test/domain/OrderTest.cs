using FluentAssertions;
using Moq;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class OrderTest
    {
        [Fact]
        public void AddScannable_ScannableIsAddedToScannedItems()
        {
            var dummyScannable = new Mock<IScannable>().Object;
            var order = new Order();

            order.AddScannable(dummyScannable);

            order.ScannedItems.Should().Contain(dummyScannable);
        }
    }
}