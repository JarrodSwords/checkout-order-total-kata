using NodaMoney;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public static class MarkdownProvider
    {
        public static Markdown ActiveMarkdown(decimal amountOffRetail) =>
            new Markdown(new Money(amountOffRetail), DateTimeProvider.Now().StartOfWeek(), DateTimeProvider.Now().EndOfWeek());
    
        public static Markdown ExpiredMarkdown(decimal amountOffRetail) =>
            new Markdown(new Money(amountOffRetail), DateTimeProvider.Now().StartOfLastWeek(), DateTimeProvider.Now().EndOfLastWeek());
    
        public static Markdown FutureMarkdown(decimal amountOffRetail) =>
            new Markdown(new Money(amountOffRetail), DateTimeProvider.Now().StartOfNextWeek(), DateTimeProvider.Now().EndOfNextWeek());
    }
}