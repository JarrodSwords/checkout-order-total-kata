namespace PointOfSale.Services
{
    public class RemoveScannedItemArgs : IOrderIdArgs, IScannedItemIdArgs
    {
        public long? OrderId { get; set; }
        public int? ScannedItemId { get; set; }
    }
}
