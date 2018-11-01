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
            var eachesLineItemFactory = new EachesLineItemFactory();
            var weightedLineItemFactory = new WeightedLineItemFactory();
            var markdownLineItemFactory = new MarkdownLineItemFactory();
            var weightedMarkdownLineItemFactory = new WeightedMarkdownLineItemFactory();

            foreach (var scannable in scannedItems.Where(x => x.Product.SellByType == SellByType.Unit))
            {
                eachesLineItemFactory.Configure(scannable);
                lineItems.Add(eachesLineItemFactory.CreateLineItem());

                if (scannable.Product.Markdown == null || scannable.Product.Markdown.IsActive == false)
                    continue;

                markdownLineItemFactory.Configure(scannable);
                lineItems.Add(markdownLineItemFactory.CreateLineItem());
            }

            foreach (var scannable in scannedItems.Where(x => x.Product.SellByType == SellByType.Weight))
            {
                weightedLineItemFactory.Configure((WeightedItem)scannable);
                lineItems.Add(weightedLineItemFactory.CreateLineItem());

                if (scannable.Product.Markdown == null || scannable.Product.Markdown.IsActive == false)
                    continue;

                weightedMarkdownLineItemFactory.Configure((WeightedItem)scannable);
                lineItems.Add(weightedMarkdownLineItemFactory.CreateLineItem());
            }

            return lineItems;
        }
    }
}