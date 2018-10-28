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

        public static Money CalculatePreTaxTotal(ICollection<LineItem> lineItems)
        {
            return Money.USDollar(lineItems.Sum(x => x.SalePrice.Amount));
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