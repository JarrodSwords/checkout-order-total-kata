using System.Collections.Generic;
using System.Linq;

namespace PointOfSale.Domain
{
    public partial class Invoice
    {
        public class Factory : IInvoiceFactory
        {
            public Order Order { get; set; }

            public Factory(Order order)
            {
                Order = order;
            }

            public Invoice CreateInvoice()
            {
                var lineItems = CreateLineItems(Order.ScannedItems);
                return new Invoice(Order.Id, lineItems);
            }

            public static ICollection<LineItem> CreateLineItems(IEnumerable<ScannedItem> scannedItems)
            {
                var lineItems = CreateRetailLineItems(scannedItems).ToList();

                foreach (var product in scannedItems.Select(x => x.Product).Distinct())
                    lineItems.AddRange(CreateProductDiscountLineItems(product, scannedItems.Where(x => x.Product == product)));

                return lineItems;
            }

            public static ICollection<LineItem> CreateProductDiscountLineItems(Product product, IEnumerable<ScannedItem> scannedItems)
            {
                var lineItems = new List<LineItem>();

                if (!product.HasActiveMarkdown && !product.HasActiveSpecial)
                    return lineItems;

                if (product.HasActiveSpecial)
                    lineItems.AddRange(CreateProductSpecialLineItems(scannedItems));

                if (product.HasActiveMarkdown)
                {
                    var discountedScannedItemIds = lineItems.SelectMany(x => ((SpecialLineItem) x).LineItemIds).ToList();
                    var remainingItems = scannedItems.Where(x => !discountedScannedItemIds.Contains(x.Id));
                    lineItems.AddRange(CreateProductMarkdownLineItems(remainingItems));
                }

                return lineItems;
            }

            public static IEnumerable<LineItem> CreateProductMarkdownLineItems(IEnumerable<ScannedItem> scannedItems) =>
                scannedItems.Select(x => x.CreateMarkdownLineItem());

            public static IEnumerable<LineItem> CreateProductSpecialLineItems(IEnumerable<ScannedItem> scannedItems)
            {
                var product = scannedItems.First().Product;
                return product.Special.CreateLineItems(scannedItems);
            }

            public static IEnumerable<LineItem> CreateRetailLineItems(IEnumerable<ScannedItem> scannedItems) =>
                scannedItems.Select(x => x.CreateRetailLineItem());
        }
    }
}
