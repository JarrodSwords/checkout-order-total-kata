namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public class RemoveScannedItemArgs
    {
        public long? OrderId { get; set; }
        public int? ItemId { get; set; }

        public RemoveScannedItemArgs() { }

        public RemoveScannedItemArgs(long? orderId, int? itemId)
        {
            OrderId = orderId;
            ItemId = itemId;
        }
    }
}