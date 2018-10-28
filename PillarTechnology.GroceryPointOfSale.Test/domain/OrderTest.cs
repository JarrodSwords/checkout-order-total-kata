using System.Linq;
using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class OrderTest
    {
        private Order _order;
        private ScannedItemTestData _scannedItemTestData = new ScannedItemTestData();

        public OrderTest()
        {
            _order = new Order();
        }

        [Fact]
        public void AddScannable_ScannableIsAddedToScannedItems()
        {
            var scannedItem = _scannedItemTestData.GetScannable();

            _order.AddScannable(scannedItem);

            _order.ScannedItems.Should().Contain(scannedItem);
        }

        [Fact]
        public void AddMultipleScannables_AllScannedItemIdsAreUnique()
        {
            var items = new ScannedItemTestData().GetEnumerator();

            while (items.MoveNext())
                _order.AddScannable(items.Current);

            _order.ScannedItems.Should().OnlyHaveUniqueItems(x => x.Id);
        }

        [Fact]
        public void RemoveScannable_ScannableIsRemovedFromScannedItems()
        {
            var order = OrderTestData.CreateOrderWithScannedItems();
            var itemId = order.ScannedItems.Select(x => x.Id).First();

            var removedItem = order.RemoveScannedItem(itemId);

            order.ScannedItems.Should().NotContain(removedItem);
        }

        [Fact]
        public void OnScannedItemsCollectionChanged_CreatesNewInvoice()
        {
            var invoice = _order.Invoice;
            _order.AddScannable(_scannedItemTestData.GetScannable());

            _order.Invoice.Should().NotBe(invoice);
        }
    }
}