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
        public void CreateLineItems_CreatesALineItemPerScannedItem()
        {
            var lineItems = new InvoiceFactory(_order).CreateLineItems();

            var lineItemScannedItemIds = lineItems.Where(x => x.ScannedItemId != null).Select(x => x.ScannedItemId).ToList();
            lineItemScannedItemIds.Should().OnlyHaveUniqueItems();
            lineItemScannedItemIds.Should().BeEquivalentTo(_order.ScannedItems.Select(x => x.Id));
        }
    }
}