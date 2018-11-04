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
        public void CreateRetailLineItems_CreatesOneRetailLineItemPerScannedItem()
        {
            AddFullSetOfScannedItemsToOrder();
            var lineItems = InvoiceFactory.CreateRetailLineItems(_order.ScannedItems);

            var lineItemScannedItemIds = lineItems.Select(x => ((RetailLineItem) x).ScannedItemId).ToList();
            lineItemScannedItemIds.Should().OnlyHaveUniqueItems();
            lineItemScannedItemIds.Should().BeEquivalentTo(_order.ScannedItems.Select(x => x.Id));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10)]
        public void CreateProductMarkdownLineItems_CreatesOneMarkdownLineItemPerScannedItem(int scannedItemCount)
        {
            var product = new Product("product with markdown", Money.USDollar(1m), SellByType.Unit)
            {
                Markdown = MarkdownProvider.GetMarkdown(0.5m, DateRange.Active)
            };

            for (var i = 0; i < scannedItemCount; i++)
                _order.AddScannedItem(new ScannedItem(product));

            var lineItems = InvoiceFactory.CreateProductMarkdownLineItems(_order.ScannedItems).ToList();

            var lineItemScannedItemIds = lineItems.Select(x => ((MarkdownLineItem) x).ScannedItemId).ToList();
            lineItemScannedItemIds.Should().OnlyHaveUniqueItems();
            lineItemScannedItemIds.Should().BeEquivalentTo(_order.ScannedItems.Select(x => x.Id));
        }
    }
}
