using System;
using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public interface IProductConfigurationService
    {
        ProductDto CreateProduct(ProductDto productDto);
        ProductDto UpdateProduct(ProductDto productDto);
    }
}