using System;
using NodaMoney;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public static class MarkdownProvider
    {
        public static Markdown GetMarkdown(Money amountOffRetail, DateRange dateRange)
        {
            var now = DateTime.Now;
            return new Markdown(amountOffRetail, dateRange.GetStart(now), dateRange.GetEnd(now));
        }
    }
}
