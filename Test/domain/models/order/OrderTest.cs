using System.Linq;
using FluentAssertions;
using PointOfSale.Domain;
using Xunit;

namespace PointOfSale.Test
{
    public class OrderTest
    {
        private Order _order = OrderProvider.CreateOrderWithScannedItems();

        [Fact]
        public void AddScannedItem_ScannedItemIsAddedToScannedItemsAndNewInvoiceIsCreated()
        {
            var invoice = _order.Invoice;
            var scannedItem = new ScannedItemProvider().GetScannedItem();

            _order.AddScannedItem(scannedItem);

            _order.ScannedItems.Should().Contain(scannedItem);
            _order.Invoice.Should().NotBe(invoice);
        }

        [Fact]
        public void AddMultipleScannedItems_AllScannedItemIdsAreUnique()
        {
            var order = new Order();

            new ScannedItemProvider().ScannedItems.ToList().ForEach(x => order.AddScannedItem(x));

            order.ScannedItems.Should().OnlyHaveUniqueItems(x => x.Id);
        }

        [Fact]
        public void RemoveScannedItem_ScannedItemIsRemovedFromScannedItemsAndNewInvoiceIsCreated()
        {
            var invoice = _order.Invoice;
            var itemId = _order.ScannedItems.Select(x => x.Id).First();

            var removedItem = _order.RemoveScannedItem(itemId);

            _order.ScannedItems.Should().NotContain(removedItem);
            _order.Invoice.Should().NotBe(invoice);
        }
    }
}