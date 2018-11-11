namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public class RemoveScannedItemArgs
    {
        public long? OrderId { get; set; }
        public int? ScannedItemId { get; set; }

        public RemoveScannedItemArgs() { }

        public RemoveScannedItemArgs(long? orderId, int? scannedItemId)
        {
            OrderId = orderId;
            ScannedItemId = scannedItemId;
        }
    }
}