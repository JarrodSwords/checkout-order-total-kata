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

        public static ICollection<LineItem> CreateProductDiscountLineItems(Product product, IEnumerable<ScannedItem> scannedItems)
        {
            var lineItems = new List<LineItem>();

            if ((product.Markdown == null || !product.Markdown.IsActive) &&
                (product.Special == null || !product.Special.IsActive))
                return lineItems;

            if (product.Special != null && product.Special.IsActive)
                lineItems.AddRange(InvoiceFactory.CreateProductSpecialLineItems(product, scannedItems));

            return lineItems;
        }

        public static IEnumerable<LineItem> CreateProductMarkdownLineItems(IEnumerable<ScannedItem> scannedItems)
        {
            return scannedItems.Select(x => x.CreateMarkdownLineItem());
        }

        public static IEnumerable<LineItem> CreateProductSpecialLineItems(Product product, IEnumerable<ScannedItem> scannedItems)
        {
            var productSpecial = new ProductSpecial(product, product.Special);
            return productSpecial.CreateLineItems(scannedItems);
        }

        public static IEnumerable<LineItem> CreateRetailLineItems(IEnumerable<ScannedItem> scannedItems)
        {
            return scannedItems.Select(x => x.CreateRetailLineItem());
        }
    }
}
