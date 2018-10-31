using System;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public class UpsertProductMarkdownDto
    {
        public string ProductName { get; set; }
        public decimal? AmountOffRetail { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public UpsertProductMarkdownDto()
        { 
        }

        public UpsertProductMarkdownDto(string productName, decimal? amountOffRetail, DateTime startTime, DateTime endTime)
        {
            ProductName = productName;
            AmountOffRetail = amountOffRetail;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}