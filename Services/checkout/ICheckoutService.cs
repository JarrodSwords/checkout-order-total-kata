namespace PointOfSale.Services
{
    public interface ICheckoutService
    {
        ScannedItemDto RemoveScannedItem(RemoveScannedItemArgs args);
        ScannedItemAsEachesDto ScanItem(ScanItemArgs args);
        ScannedItemWithMassDto ScanWeightedItem(ScanWeightedItemArgs args);
    }
}
