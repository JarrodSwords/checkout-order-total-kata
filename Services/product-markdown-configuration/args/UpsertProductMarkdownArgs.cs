using System;

namespace PointOfSale.Services
{
    public class UpsertProductMarkdownArgs : IAmountOffRetailArgs, IProductNameArgs, ISellByTypeArgs, ITemporalArgs
    {
        public decimal? AmountOffRetail { get; set; }
        public DateTime? EndTime { get; set; }
        public string ProductName { get; set; }
        public string SellByType { get; set; }
        public DateTime? StartTime { get; set; }

        public UpsertProductMarkdownArgs(decimal? amountOffRetail, DateTime? endTime, string productName, string sellByType, DateTime? startTime)
        {
            AmountOffRetail = amountOffRetail;
            EndTime = endTime;
            ProductName = productName;
            SellByType = sellByType;
            StartTime = startTime;
        }
    }
}
