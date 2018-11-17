using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NodaMoney;
using PointOfSale.Domain;
using Xunit;

namespace PointOfSale.Test.Domain
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

            var lineItems = Invoice.Factory.CreateProductDiscountLineItems(product, _order.ScannedItems);
            lineItems.Count().Should().Be(0);
        }

        [Theory]
        [MemberData(nameof(SpecialsAndExpectedLineItems))]
        public void CreateProductDiscountLineItems_CreatesSpecialLineItems(decimal retailPrice, Special special, int scannedItemCount, int expectedLineItemCount, decimal expectedValue)
        {
            var product = new Product("product with special", Money.USDollar(retailPrice), SellByType.Unit) { Special = special };

            PopulateOrder(product, scannedItemCount);
            var discountLineItems = Invoice.Factory.CreateProductDiscountLineItems(product, _order.ScannedItems);
            var specialLineItems = discountLineItems.Where(x => x.GetType() == typeof(SpecialLineItem)).ToList();

            specialLineItems.Count().Should().Be(expectedLineItemCount);
            specialLineItems.Sum(x => x.SalePrice.Amount).Should().Be(expectedValue);
        }

        [Theory]
        [MemberData(nameof(MarkdownsSpecialsAndExpectedLineItems))]
        public void CreateProductDiscountLineItems_CreatesMarkdownLineItems(Markdown markdown, Special special, int scannedItemCount, int expectedLineItemCount, decimal expectedValue)
        {
            var product = new Product("test product", Money.USDollar(1m), SellByType.Unit)
            {
                Markdown = markdown,
                Special = special
            };

            PopulateOrder(product, scannedItemCount);
            var discountLineItems = Invoice.Factory.CreateProductDiscountLineItems(product, _order.ScannedItems);
            var markdownLineItems = discountLineItems.Where(x => x.GetType() == typeof(MarkdownLineItem)).ToList();

            markdownLineItems.Count().Should().Be(expectedLineItemCount);
            markdownLineItems.Sum(x => x.SalePrice.Amount).Should().Be(expectedValue);
        }

        [Theory]
        [InlineData(2, 1, 4, 4, 9.8)]
        [InlineData(1, 2, 6, 5, 12.1)]
        public void CreateInvoice_CalculatesPreTaxTotal(int product1Count, int product2Count, int product3Count, int product4Count, double expectedTotal)
        {
            var markdown = MarkdownProvider.GetMarkdown(DateRange.Active, 0.10m);
            var special = SpecialProvider.GetBuyNGetMAtXPercentOffSpecial(DateRange.Active, 2, 1, 50m);

            var product1 = new Product("product", Money.USDollar(1m), SellByType.Unit);
            var product2 = new Product("product with markdown", Money.USDollar(1m), SellByType.Unit) { Markdown = markdown };
            var product3 = new Product("product with special", Money.USDollar(1m), SellByType.Unit) { Special = special };
            var product4 = new Product("product with markdown and special", Money.USDollar(1m), SellByType.Unit) { Markdown = markdown, Special = special };

            PopulateOrder(product1, product1Count);
            PopulateOrder(product2, product2Count);
            PopulateOrder(product3, product3Count);
            PopulateOrder(product4, product4Count);

            _order.Invoice.PreTaxTotal.Amount.Should().Be((decimal)expectedTotal);
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

            var lineItems = Invoice.Factory.CreateProductMarkdownLineItems(_order.ScannedItems).ToList();

            var lineItemScannedItemIds = lineItems.Select(x => ((MarkdownLineItem) x).ScannedItemId).ToList();
            lineItemScannedItemIds.Should().OnlyHaveUniqueItems();
            lineItemScannedItemIds.Should().BeEquivalentTo(_order.ScannedItems.Select(x => x.Id));
        }

        [Fact]
        public void CreateRetailLineItems_CreatesOneRetailLineItemPerScannedItem()
        {
            AddFullSetOfScannedItemsToOrder();
            var lineItems = Invoice.Factory.CreateRetailLineItems(_order.ScannedItems);

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

        public static IEnumerable<object[]> MarkdownsSpecialsAndExpectedLineItems()
        {
            var special = SpecialProvider.GetBuyNGetMAtXPercentOffSpecial(DateRange.Active, 2, 1, 50m);
            var markdown = MarkdownProvider.GetMarkdown(DateRange.Active, 0.1m);

            yield return new object[] { null, special, 3, 0, 0 };
            yield return new object[] { markdown, special, 3, 0, 0 };
            yield return new object[] { markdown, special, 5, 2, -0.2m };
        }

        #endregion
    }
}
