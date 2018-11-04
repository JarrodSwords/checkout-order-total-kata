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
        public void CreateLineItems_CreatesARetailLineItemPerScannedItem()
        {
            var lineItems = new InvoiceFactory(_order).CreateLineItems();

            var lineItemScannedItemIds = lineItems
                .Where(x => x.GetType() == typeof(RetailLineItem))
                .Select(x => ((RetailLineItem) x).ScannedItemId).ToList();

            lineItemScannedItemIds.Should().OnlyHaveUniqueItems();
            lineItemScannedItemIds.Should().BeEquivalentTo(_order.ScannedItems.Select(x => x.Id));
        }
    }
}
