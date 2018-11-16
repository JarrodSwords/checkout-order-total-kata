using System;
using NodaMoney;

namespace GroceryPointOfSale.Domain
{
    public interface IMarkdownFactory
    {
        IMarkdownFactory Configure(Money amountOffRetail, DateTime endTime, DateTime startTime);
        Markdown CreateMarkdown();
    }
}
