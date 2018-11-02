namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public class ScanWeightedItemArgs
    {
        public long OrderId { get; set; }
        public string ProductName { get; set; }
        public decimal Weight { get; set; }
    }
}