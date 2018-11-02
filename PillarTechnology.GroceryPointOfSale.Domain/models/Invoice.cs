using System;
using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class Invoice
    {
        public long OrderId { get; }
        public ICollection<LineItem> LineItems { get; }
        public Money PreTaxTotal { get; set; }

        public Invoice(Order order)
        {
            OrderId = order.Id;
            LineItems = CreateLineItems(order.ScannedItems);
            PreTaxTotal = CalculatePreTaxTotal(LineItems);
        }

        public override string ToString()
        {
            return $"Order: {OrderId}, Pre-tax total: {PreTaxTotal}";
        }

        public static Money CalculatePreTaxTotal(ICollection<LineItem> lineItems)
        {
            return Money.USDollar(lineItems.Sum(x => x.SalePrice.Amount));
        }

        public static ICollection<LineItem> CreateLineItems(IEnumerable<IScannable> scannedItems)
        {
            var lineItems = new List<LineItem>();

            foreach (var product in scannedItems.Select(x => x.Product).Distinct())
            {
                var items = scannedItems.Where(x => x.Product == product);

                lineItems.AddRange(CreateScannedItemLineItems(items));

                if (product.Markdown == null || !product.Markdown.IsActive)
                    continue;

                lineItems.AddRange(CreateMarkdownLineItems(items));
            }

            return lineItems;
        }

        private static IEnumerable<LineItem> CreateScannedItemLineItems(IEnumerable<IScannable> scannedItems)
        {
            foreach (var scannable in scannedItems)
            {
                var salePrice = scannable.Product.SellByType == SellByType.Unit ?
                    scannable.Product.RetailPrice :
                    scannable.Product.RetailPrice * ((WeightedItem) scannable).Weight;

                yield return new LineItem(scannable.Product.Name, salePrice, scannable.Id);
            }
        }

        private static IEnumerable<LineItem> CreateMarkdownLineItems(IEnumerable<IScannable> scannedItems)
        {
            foreach (var scannable in scannedItems)
            {
                var salePrice = scannable.Product.SellByType == SellByType.Unit ?
                    scannable.Product.Markdown.AmountOffRetail :
                    scannable.Product.Markdown.AmountOffRetail * ((WeightedItem) scannable).Weight;

                yield return new LineItem(scannable.Product.Name + " markdown", -scannable.Product.Markdown.AmountOffRetail);
            }
        }
    }
}