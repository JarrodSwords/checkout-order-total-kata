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

        private static IEnumerable<LineItem> CreateScannedItemLineItems(IEnumerable<ScannedItem> scannedItems)
        {
            foreach (var scannedItem in scannedItems)
            {
                var salePrice = scannedItem.Product.SellByType == SellByType.Unit ?
                    scannedItem.Product.RetailPrice :
                    scannedItem.Product.RetailPrice * ((ScannedWeightedItem) scannedItem).Weight;

                yield return new LineItem(scannedItem.Product.Name, salePrice, scannedItem.Id);
            }
        }

        private static IEnumerable<LineItem> CreateMarkdownLineItems(IEnumerable<ScannedItem> scannedItems)
        {
            foreach (var scannedItem in scannedItems)
            {
                var salePrice = scannedItem.Product.SellByType == SellByType.Unit ?
                    scannedItem.Product.Markdown.AmountOffRetail :
                    scannedItem.Product.Markdown.AmountOffRetail * ((ScannedWeightedItem) scannedItem).Weight;

                yield return new LineItem(scannedItem.Product.Name + " markdown", -scannedItem.Product.Markdown.AmountOffRetail);
            }
        }
    }
}