using System.Linq;
using FluentAssertions;
using NodaMoney;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class InvoiceFactoryTest
    {
        private readonly Order _order = OrderProvider.CreateOrderWithScannedItems();

        [Fact]
        public void CreateRetailLineItems_CreatesOneLineItemPerScannedItem()
        {
            var lineItems = InvoiceFactory.CreateRetailLineItems(_order.ScannedItems);

            var lineItemScannedItemIds = lineItems.Select(x => ((RetailLineItem) x).ScannedItemId).ToList();
            lineItemScannedItemIds.Should().OnlyHaveUniqueItems();
            lineItemScannedItemIds.Should().BeEquivalentTo(_order.ScannedItems.Select(x => x.Id));
        }
    }
}
