using System;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public interface ICheckoutService
    {
        ScannedItem RemoveScannedItem(long orderId, int itemId);
        ScannedItem ScanItem(ScanItemArgs args);
        ScannedItem ScanWeightedItem(ScanWeightedItemArgs args);
    }
}