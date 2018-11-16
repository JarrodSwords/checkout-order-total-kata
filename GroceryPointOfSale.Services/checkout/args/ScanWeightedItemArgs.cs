namespace GroceryPointOfSale.ApplicationServices
{
    public class ScanWeightedItemArgs
    {
        public long? OrderId { get; set; }
        public string ProductName { get; set; }
        public decimal? Weight { get; set; }

        public ScanWeightedItemArgs() { }

        public ScanWeightedItemArgs(long? orderId, string productName, decimal? weight)
        {
            OrderId = orderId;
            ProductName = productName;
            Weight = weight;
        }
    }
}