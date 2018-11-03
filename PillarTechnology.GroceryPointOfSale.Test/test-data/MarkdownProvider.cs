using System;
using NodaMoney;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public static class MarkdownProvider
    {
        public static Markdown ActiveMarkdown(decimal amountOffRetail) =>
            new Markdown(new Money(amountOffRetail), DateTime.Now.StartOfWeek(), DateTime.Now.EndOfWeek());
    
        public static Markdown ExpiredMarkdown(decimal amountOffRetail) =>
            new Markdown(new Money(amountOffRetail), DateTime.Now.StartOfLastWeek(), DateTime.Now.EndOfLastWeek());
    
        public static Markdown FutureMarkdown(decimal amountOffRetail) =>
            new Markdown(new Money(amountOffRetail), DateTime.Now.StartOfNextWeek(), DateTime.Now.EndOfNextWeek());
    }
}