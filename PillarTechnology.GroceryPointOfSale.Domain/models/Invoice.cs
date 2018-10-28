using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class Invoice
    {
        public long OrderId { get; }
        public ICollection<LineItem> LineItems { get; }

        public Invoice(Order order)
        {
            OrderId = order.Id;
            LineItems = CreateLineItems(order.ScannedItems);
        }

        public static ICollection<LineItem> CreateLineItems(IEnumerable<IScannable> scannedItems)
        {
            var lineItems = scannedItems.Where(x => x.Product.SellByType == SellByType.Unit)
                .Select(x => new EachesLineItemFactory(x).CreateLineItem()).ToList();

            lineItems.AddRange(scannedItems.Where(x => x.Product.SellByType == SellByType.Weight)
                .Select(x => new WeightedLineItemFactory((WeightedItem) x).CreateLineItem()).ToList()
            );

            return lineItems;
        }
    }
}