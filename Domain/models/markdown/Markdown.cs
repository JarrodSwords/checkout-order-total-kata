using System;
using NodaMoney;

namespace PointOfSale.Domain
{
    /// <summary>
    /// A flat discount
    /// </summary>
    /// <remarks>
    /// Value object; immutable;
    /// </remarks>
    public class Markdown : ITemporal
    {
        private readonly ITemporal _temporal;
        public Money AmountOffRetail { get; }
        public DateTime EndTime => _temporal.EndTime;
        public bool IsActive => _temporal.IsActive;
        public DateTime StartTime => _temporal.StartTime;

        public Markdown(Money amountOffRetail, DateTime endTime, DateTime startTime)
        {
            AmountOffRetail = amountOffRetail;
            _temporal = new Temporal(endTime, startTime);
        }
    }
}
