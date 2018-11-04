using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NodaMoney;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class InvoiceFactoryTest
    {
        private readonly Order _order = new Order();

        private void AddFullSetOfScannedItemsToOrder()
        {
            foreach (var product in ProductProvider.GetOneOfEachProduct())
                _order.AddScannedItem(new ScannedItem(product));
        }

        [Fact]
        public void CreateRetailLineItems_CreatesOneLineItemPerScannedItem()
        {
            AddFullSetOfScannedItemsToOrder();
            var lineItems = InvoiceFactory.CreateRetailLineItems(_order.ScannedItems);

            var lineItemScannedItemIds = lineItems.Select(x => ((RetailLineItem) x).ScannedItemId).ToList();
            lineItemScannedItemIds.Should().OnlyHaveUniqueItems();
            lineItemScannedItemIds.Should().BeEquivalentTo(_order.ScannedItems.Select(x => x.Id));
        }
    }
}
