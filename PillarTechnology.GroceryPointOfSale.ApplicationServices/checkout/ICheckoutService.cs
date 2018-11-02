using System;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public interface ICheckoutService
    {
        ScannedItemDto RemoveScannedItem(RemoveScannedItemArgs args);
        ScannedItemDto ScanItem(ScanItemArgs args);
        ScannedItemDto ScanWeightedItem(ScanWeightedItemArgs args);
    }
}