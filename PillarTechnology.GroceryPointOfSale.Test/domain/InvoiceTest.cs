using System.Linq;
using FluentAssertions;
using Moq;
using NodaMoney;
using PillarTechnology.GroceryPointOfSale.Domain;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class InvoiceTest
    {
        private readonly Order _order = OrderTestData.CreateOrderWithScannedItems();

        [Fact]
        public void CalculatePreTaxTotal_ReturnsSumOfAllLineItems()
        {
            var lineItems = Invoice.CreateLineItems(_order.ScannedItems);
            var preTaxTotal = Invoice.CalculatePreTaxTotal(lineItems);

            preTaxTotal.Should().Be(Money.USDollar(lineItems.Sum(x => x.SalePrice.Amount)));
        }

        [Fact]
        public void CreateLineItems_CreatesALineItemPerScannedItem()
        {
            var lineItems = Invoice.CreateLineItems(_order.ScannedItems);

            var lineItemScannedItemIds = lineItems.Select(x => x.ScannedItemId);
            lineItemScannedItemIds.Should().OnlyHaveUniqueItems();
            lineItemScannedItemIds.Should().BeEquivalentTo(_order.ScannedItems.Select(x => x.Id));
        }
    }
}