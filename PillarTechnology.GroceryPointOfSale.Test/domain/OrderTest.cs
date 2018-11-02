using System.Linq;
using FluentAssertions;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class OrderTest
    {
        private Order _order = OrderProvider.CreateOrderWithScannedItems();

        [Fact]
        public void AddScannable_ScannableIsAddedToScannedItemsAndNewInvoiceIsCreated()
        {
            var invoice = _order.Invoice;
            var scannedItem = new ScannedItemProvider().GetScannable();

            _order.AddScannable(scannedItem);

            _order.ScannedItems.Should().Contain(scannedItem);
            _order.Invoice.Should().NotBe(invoice);
        }

        [Fact]
        public void AddMultipleScannables_AllScannedItemIdsAreUnique()
        {
            var order = new Order();

            new ScannedItemProvider().ScannedItems.ToList().ForEach(x => order.AddScannable(x));

            order.ScannedItems.Should().OnlyHaveUniqueItems(x => x.Id);
        }

        [Fact]
        public void RemoveScannable_ScannableIsRemovedFromScannedItemsAndNewInvoiceIsCreated()
        {
            var invoice = _order.Invoice;
            var itemId = _order.ScannedItems.Select(x => x.Id).First();

            var removedItem = _order.RemoveScannedItem(itemId);

            _order.ScannedItems.Should().NotContain(removedItem);
            _order.Invoice.Should().NotBe(invoice);
        }
    }
}