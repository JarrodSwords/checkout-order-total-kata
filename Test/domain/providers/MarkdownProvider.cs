using System;
using NodaMoney;
using PointOfSale.Domain;

namespace PointOfSale.Test.Domain
{
    public static class MarkdownProvider
    {
        public static Markdown GetMarkdown(DateRange dateRange, decimal amountOffRetail = 0.5m)
        {
            var dateTimeProvider = new BasicDateTimeProvider();
            var markdownFactory = new Markdown.Factory(dateTimeProvider);
            var now = dateTimeProvider.Now;

            return markdownFactory
                .Configure(Money.USDollar(amountOffRetail), dateRange.GetEnd(now), dateRange.GetStart(now))
                .CreateMarkdown();
        }
    }
}
