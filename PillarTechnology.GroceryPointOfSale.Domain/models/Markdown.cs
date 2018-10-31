using System;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class Markdown
    {
        public Money AmountOffRetail { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public Markdown()
        {
        }

        public Markdown(Money amountOffRetail, DateTime startTime, DateTime endTime)
        {
            AmountOffRetail = amountOffRetail;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}