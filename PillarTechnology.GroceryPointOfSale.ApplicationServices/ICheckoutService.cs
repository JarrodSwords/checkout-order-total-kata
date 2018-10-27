using System;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public interface ICheckoutService
    {
        IScannable RemoveScannedItem(long orderId, int itemId);
        IScannable ScanItem(long orderId, string productName);
        IScannable ScanItem(long orderId, string productName, decimal weight);
    }
}