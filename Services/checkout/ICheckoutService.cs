﻿namespace PointOfSale.Services
{
    public interface ICheckoutService
    {
        ScannedItemDto RemoveScannedItem(RemoveScannedItemArgs args);
        ScannedItemDto ScanItem(ScanItemArgs args);
        ScannedItemDto ScanWeightedItem(ScanWeightedItemArgs args);
    }
}