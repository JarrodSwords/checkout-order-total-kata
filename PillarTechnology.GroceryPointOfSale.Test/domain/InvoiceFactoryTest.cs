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

        [Theory]
        [ClassData(typeof(OrderProvider))]
        public void CalculatePreTaxTotal_ReturnsCorrectPreTaxTotal(long orderId, decimal preTaxTotal)
        {
            var invoice = new InvoiceFactory(_order).CreateInvoice();

            invoice.PreTaxTotal.Should().Be(Money.USDollar(preTaxTotal));
        }

        [Fact]
        public void CreateLineItems_CreatesALineItemPerScannedItem()
        {
            var invoice = new InvoiceFactory(_order).CreateInvoice();

            var lineItemScannedItemIds = invoice.LineItems.Where(x => x.ScannedItemId != null).Select(x => x.ScannedItemId).ToList();
            lineItemScannedItemIds.Should().OnlyHaveUniqueItems();
            lineItemScannedItemIds.Should().BeEquivalentTo(_order.ScannedItems.Select(x => x.Id));
        }
    }
}