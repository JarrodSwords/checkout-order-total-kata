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
            return new Invoice(Order.Id, lineItems);
        }

        public ICollection<LineItem> CreateLineItems()
        {
            var lineItems = new List<LineItem>();

            foreach (var product in Order.ScannedItems.Select(x => x.Product).Distinct())
            {
                var scannedItems = Order.ScannedItems.Where(x => x.Product == product).ToList();

                lineItems.AddRange(scannedItems.Select(x => x.CreateRetailLineItem()));

                if (product.Special != null)
                    lineItems.AddRange(product.Special.CreateLineItems(scannedItems));
                else if (product.Markdown != null && product.Markdown.IsActive)
                    lineItems.AddRange(scannedItems.Select(x => x.CreateMarkdownLineItem()));
            }

            return lineItems;
        }
    }
}