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

        private void PopulateOrder(Product product, int scannedItemCount)
        {
            for (var i = 0; i < scannedItemCount; i++)
                _order.AddScannedItem(new ScannedItem(product));
        }

        [Theory]
        [MemberData(nameof(InvalidDiscountProducts))]
        public void CreateProductDiscountLineItems_WithInvalidDiscounts_CreatesZeroLineItems(Product product)
        {
            PopulateOrder(product, 5);

            var lineItems = InvoiceFactory.CreateProductDiscountLineItems(product, _order.ScannedItems);
            lineItems.Count().Should().Be(0);
        }

        [Theory]
        [MemberData(nameof(SpecialsAndExpectedLineItems))]
        public void CreateProductDiscountLineItems_CreatesSpecialLineItems(decimal retailPrice, Special special, int scannedItemCount, int expectedLineItemCount, decimal expectedValue)
        {
            var product = new Product("product with special", Money.USDollar(retailPrice), SellByType.Unit) { Special = special };
            
            PopulateOrder(product, scannedItemCount);
            var discountLineItems = InvoiceFactory.CreateProductDiscountLineItems(product, _order.ScannedItems);
            var specialLineItems = discountLineItems.Where(x => x.GetType() == typeof(SpecialLineItem)).ToList();

            specialLineItems.Count().Should().Be(expectedLineItemCount);
            specialLineItems.Sum(x => x.SalePrice.Amount).Should().Be(expectedValue);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(10)]
        public void CreateProductMarkdownLineItems_CreatesOneMarkdownLineItemPerScannedItem(int scannedItemCount)
        {
            var product = new Product("product with markdown", Money.USDollar(1m), SellByType.Unit)
            {
                Markdown = MarkdownProvider.GetMarkdown(DateRange.Active)
            };

            PopulateOrder(product, scannedItemCount);

            var lineItems = InvoiceFactory.CreateProductMarkdownLineItems(_order.ScannedItems).ToList();

            var lineItemScannedItemIds = lineItems.Select(x => ((MarkdownLineItem) x).ScannedItemId).ToList();
            lineItemScannedItemIds.Should().OnlyHaveUniqueItems();
            lineItemScannedItemIds.Should().BeEquivalentTo(_order.ScannedItems.Select(x => x.Id));
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

        #region Test data

        public static IEnumerable<object[]> InvalidDiscountProducts()
        {
            var product = new Product("test product", Money.USDollar(1m), SellByType.Unit);
            yield return new object[] { product };

            product.Markdown = MarkdownProvider.GetMarkdown(DateRange.Expired);
            yield return new object[] { product };

            product.Special = SpecialProvider.GetBuyNGetMAtXPercentOffSpecial(DateRange.Expired);
            yield return new object[] { product };

            product.Markdown = null;
            yield return new object[] { product };
        }

        public static IEnumerable<object[]> SpecialsAndExpectedLineItems()
        {
            yield return new object[] { 1, SpecialProvider.GetBuyNGetMAtXPercentOffSpecial(DateRange.Active, 2, 1, 50m), 3, 1, -0.5m };
            yield return new object[] { 2, SpecialProvider.GetBuyNGetMAtXPercentOffSpecial(DateRange.Active, 2, 3, 10m), 10, 2, -1.2m };
        }

        #endregion
    }
}
