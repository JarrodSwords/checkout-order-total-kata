using System;
using System.Collections.Generic;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class Invoice
    {
        public long OrderId { get; }
        public IEnumerable<LineItem> LineItems { get; }
        public Money PreTaxTotal { get; }

        public Invoice(long orderId, IEnumerable<LineItem> lineItems, Money preTaxTotal)
        {
            OrderId = orderId;
            LineItems = lineItems;
            PreTaxTotal = preTaxTotal;
        }

        public override string ToString()
        {
            return $"Order: {OrderId}, Pre-tax total: {PreTaxTotal}";
        }
    }
}