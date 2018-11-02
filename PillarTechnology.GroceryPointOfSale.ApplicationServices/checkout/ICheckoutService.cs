using System;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public interface ICheckoutService
    {
        ScannedItem RemoveScannedItem(RemoveScannedItemArgs args);
        ScannedItem ScanItem(ScanItemArgs args);
        ScannedItem ScanWeightedItem(ScanWeightedItemArgs args);
    }
}