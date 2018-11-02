using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class InvoiceFactory
    {
        public Order Order { get; set; }

        public InvoiceFactory(Order order)
        {
            Order = order;
        }

        public Invoice CreateInvoice()
        {
            var lineItems = CreateLineItems();
            var preTaxTotal = InvoiceFactory.CalculatePreTaxTotal(lineItems);
            return new Invoice(Order.Id, lineItems, preTaxTotal);
        }

        public static Money CalculatePreTaxTotal(ICollection<LineItem> lineItems)
        {
            return Money.USDollar(lineItems.Sum(x => x.SalePrice.Amount));
        }

        public ICollection<LineItem> CreateLineItems()
        {
            var lineItems = new List<LineItem>();

            foreach (var product in Order.ScannedItems.Select(x => x.Product).Distinct())
            {
                var items = Order.ScannedItems.Where(x => x.Product == product);

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