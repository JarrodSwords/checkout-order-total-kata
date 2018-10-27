using System;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public interface ICheckoutService
    {
        IScannable Scan(long orderId, string productName);
    }
}