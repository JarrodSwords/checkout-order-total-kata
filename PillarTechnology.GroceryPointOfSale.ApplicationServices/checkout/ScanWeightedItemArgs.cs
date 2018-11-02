namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public class ScanWeightedItemArgs : ScanItemArgs
    {
        public decimal? Weight { get; set; }

        public ScanWeightedItemArgs() { }

        public ScanWeightedItemArgs(long? orderId, string productName, decimal? weight) : base(orderId, productName)
        {
            Weight = weight;
        }
    }
}