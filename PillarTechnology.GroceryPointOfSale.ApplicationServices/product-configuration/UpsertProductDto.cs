using PillarTechnology.GroceryPointOfSale.Domain;

namespace PillarTechnology.GroceryPointOfSale.ApplicationServices
{
    public class UpsertProductDto
    {
        public string Name { get; set; }
        public decimal? RetailPrice { get; set; }
        public string SellByType { get; set; }

        public UpsertProductDto()
        {
        }

        public UpsertProductDto(string name, decimal? retailPrice, string sellByType)
        {
            Name = name;
            RetailPrice = retailPrice;
            SellByType = sellByType;
        }
    }
}