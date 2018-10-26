using System;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public interface ICheckoutService
    {
        Order Scan(long orderId, string productName);
    }
}