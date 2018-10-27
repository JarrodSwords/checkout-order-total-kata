using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public class ProductDto
    {
        public string Name { get; set; }
        public decimal? RetailPrice { get; set; }
        public SellByType SellByType { get; set; }
    }
}