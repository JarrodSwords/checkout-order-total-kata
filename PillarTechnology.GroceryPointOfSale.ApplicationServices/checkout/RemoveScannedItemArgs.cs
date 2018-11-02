namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public class RemoveScannedItemArgs
    {
        public long OrderId { get; set; }
        public int ItemId { get; set; }
    }
}