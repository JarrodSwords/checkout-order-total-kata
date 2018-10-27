using FluentAssertions;
using Moq;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class OrderTest
    {
        private Order _order;

        public OrderTest()
        {
            _order = new Order();
        }

        [Fact]
        public void AddScannable_ScannableIsAddedToScannedItems()
        {
            var dummyScannable = new Mock<IScannable>().Object;

            _order.AddScannable(dummyScannable);

            _order.ScannedItems.Should().Contain(dummyScannable);
        }

        [Fact]
        public void AddMultipleScannable_ScannedItemsHaveUniqueIds()
        {
            var items = new ScannedItemTestData().GetEnumerator();

            while (items.MoveNext())
                _order.AddScannable(items.Current);

            _order.ScannedItems.Should().OnlyHaveUniqueItems(x => x.Id);
        }
    }
}