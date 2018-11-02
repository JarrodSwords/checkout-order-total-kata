using System;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public interface ICheckoutService
    {
        ScannedItem RemoveScannedItem(long orderId, int itemId);
        ScannedItem ScanItem(long orderId, string productName);
        ScannedItem ScanItem(long orderId, string productName, decimal weight);
    }
}