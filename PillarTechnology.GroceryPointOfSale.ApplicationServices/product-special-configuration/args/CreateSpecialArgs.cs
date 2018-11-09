using System;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public class CreateSpecialArgs
    {
        public DateTime EndTime { get; set; }
        public int? Limit { get; set; }
        public string ProductName { get; set; }
        public DateTime StartTime { get; set; }
    }
}
