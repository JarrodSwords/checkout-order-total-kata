using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public decimal? RetailPrice { get; set; }
        public string SellByType { get; set; }

        public CreateProductDto()
        {
        }

        public CreateProductDto(string name, decimal retailPrice, string sellByType)
        {
            Name = name;
            RetailPrice = retailPrice;
            SellByType = sellByType;
        }
    }
}