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
        public void CalculatePreTaxTotal_ReturnsCorrectPreTaxTotal()
        {
            var invoice = new InvoiceFactory(_order).CreateInvoice();

            invoice.PreTaxTotal.Should().Be(Money.USDollar(14.75));
        }

        [Fact]
        public void CreateScannedItemLineItems_CreatesALineItemPerScannedItem()
        {
            var lineItems = InvoiceFactory.CreateScannedItemLineItems(_order.ScannedItems);

            var lineItemScannedItemIds = lineItems.Select(x => x.ScannedItemId).ToList();
            lineItemScannedItemIds.Should().OnlyHaveUniqueItems();
            lineItemScannedItemIds.Should().BeEquivalentTo(_order.ScannedItems.Select(x => x.Id));
        }
    }
}