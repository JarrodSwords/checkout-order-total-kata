using System.Collections.Generic;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public interface IProductService
    {
        ProductDto FindProduct(string productName);
    }
}