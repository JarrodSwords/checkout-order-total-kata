using System;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class Markdown : ITemporal
    {
        private readonly ITemporal _temporal;

        public Money AmountOffRetail { get; set; }
        public DateTime EndTime => _temporal.EndTime;
        public bool IsActive => _temporal.IsActive;
        public DateTime StartTime => _temporal.StartTime;

        public Markdown(Money amountOffRetail, DateTime startTime, DateTime endTime)
        {
            AmountOffRetail = amountOffRetail;
            _temporal = new BasicTemporal(endTime, startTime);
        }

        public override string ToString() => $"{AmountOffRetail}; {StartTime}-{EndTime}";
    }
}
