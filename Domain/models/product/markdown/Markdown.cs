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
    public partial class Markdown : ITemporal
    {
        private readonly ITemporal _temporal;
        public Money AmountOffRetail { get; }
        public DateTime EndTime => _temporal.EndTime;
        public bool IsActive => _temporal.IsActive;
        public DateTime StartTime => _temporal.StartTime;

        private Markdown(Money amountOffRetail, ITemporal temporal)
        {
            AmountOffRetail = amountOffRetail;
            _temporal = temporal;
        }

        public override string ToString() => $"{AmountOffRetail}; {IsActive}";
    }
}
