using System.Linq;
using FluentAssertions;
using Moq;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class InvoiceTest
    {
        [Fact]
        public void CreateLineItems_CreatesALineItemPerScannedItem()
        {
            var order = OrderTestData.CreateOrderWithScannedItems();
            var lineItems = Invoice.CreateLineItems(order.ScannedItems);

            var lineItemScannedItemIds = lineItems.Select(x => x.ScannedItemId);
            lineItemScannedItemIds.Should().OnlyHaveUniqueItems();
            lineItemScannedItemIds.Should().BeEquivalentTo(order.ScannedItems.Select(x => x.Id));
        }
    }
}