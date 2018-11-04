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
            return new Invoice(Order.Id, new List<LineItem>());
        }

        public static IEnumerable<LineItem> CreateProductDiscountLineItems(Product product, IEnumerable<ScannedItem> scannedItems)
        {
            if ((product.Markdown == null || !product.Markdown.IsActive) &&
                (product.Special == null || !product.Special.IsActive))
                yield break;
        }

        public static IEnumerable<LineItem> CreateProductMarkdownLineItems(IEnumerable<ScannedItem> scannedItems)
        {
            return scannedItems.Select(x => x.CreateMarkdownLineItem());
        }

        public static IEnumerable<LineItem> CreateRetailLineItems(IEnumerable<ScannedItem> scannedItems)
        {
            return scannedItems.Select(x => x.CreateRetailLineItem());
        }
    }
}
