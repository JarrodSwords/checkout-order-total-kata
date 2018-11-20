using System;

namespace PointOfSale.Services
{
    public class UpsertProductMarkdownArgs : ITemporalArgs
    {
        public decimal? AmountOffRetail { get; set; }
        public DateTime? EndTime { get; set; }
        public string ProductName { get; set; }
        public DateTime? StartTime { get; set; }

        public UpsertProductMarkdownArgs() { }

        public UpsertProductMarkdownArgs(string productName, decimal? amountOffRetail, DateTime? startTime, DateTime? endTime)
        {
            ProductName = productName;
            AmountOffRetail = amountOffRetail;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
