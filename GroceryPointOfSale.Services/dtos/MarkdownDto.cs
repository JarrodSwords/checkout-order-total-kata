using System;

namespace GroceryPointOfSale.ApplicationServices
{
    public class MarkdownDto
    {
        public decimal AmountOffRetail { get; set; }
        public DateTime EndTime { get; set; }
        bool IsActive { get; set; }
        public DateTime StartTime { get; set; }
    }
}
