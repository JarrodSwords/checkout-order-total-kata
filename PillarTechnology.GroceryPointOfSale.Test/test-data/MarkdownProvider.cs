using NodaMoney;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public static class MarkdownProvider
    {
        private static IDateTimeNowProvider _dateTimeNowProvider = new BasicDateTimeNowProvider();

        public static Markdown ActiveMarkdown(decimal amountOffRetail) =>
            new Markdown(new Money(amountOffRetail), _dateTimeNowProvider.Now().StartOfWeek(), _dateTimeNowProvider.Now().EndOfWeek());
    
        public static Markdown ExpiredMarkdown(decimal amountOffRetail) =>
            new Markdown(new Money(amountOffRetail), _dateTimeNowProvider.Now().StartOfLastWeek(), _dateTimeNowProvider.Now().EndOfLastWeek());
    
        public static Markdown FutureMarkdown(decimal amountOffRetail) =>
            new Markdown(new Money(amountOffRetail), _dateTimeNowProvider.Now().StartOfNextWeek(), _dateTimeNowProvider.Now().EndOfNextWeek());
    }
}