using System.Collections.Generic;
using System.Linq;
using NodaMoney;

namespace PointOfSale.Domain
{
    public partial class Invoice
    {
        public long OrderId { get; }
        public IEnumerable<LineItem> LineItems { get; }
        public Money PreTaxTotal { get; }

        private Invoice(long orderId, IEnumerable<LineItem> lineItems)
        {
            OrderId = orderId;
            LineItems = lineItems;
            PreTaxTotal = CalculatePreTaxTotal(lineItems);
        }

        public static Money CalculatePreTaxTotal(IEnumerable<LineItem> lineItems) =>
            Money.USDollar(lineItems.Sum(x => x.SalePrice.Amount));
    }
}
