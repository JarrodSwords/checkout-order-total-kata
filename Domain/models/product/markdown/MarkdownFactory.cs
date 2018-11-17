using System;
using NodaMoney;

namespace PointOfSale.Domain
{
    public partial class Markdown
    {
        public class Factory : IMarkdownFactory
        {
            private readonly IDateTimeProvider _dateTimeProvider;
            private Money AmountOffRetail { get; set; }
            private DateTime EndTime { get; set; }
            private DateTime StartTime { get; set; }

            public Factory(IDateTimeProvider dateTimeProvider)
            {
                _dateTimeProvider = dateTimeProvider;
            }

            public IMarkdownFactory Configure(Money amountOffRetail, DateTime endTime, DateTime startTime)
            {
                AmountOffRetail = amountOffRetail;
                EndTime = endTime;
                StartTime = startTime;

                return this;
            }

            public Markdown CreateMarkdown()
            {
                return new Markdown(AmountOffRetail, new BasicTemporal(EndTime, StartTime));
            }
        }
    }
}
