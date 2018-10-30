using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    /// <summary>
    /// A good available for sale
    /// </summary>
    public class Product
    {
        private string _name;

        public string Name { get { return _name; } }
        public Money RetailPrice { get; set; }
        public SellByType SellByType { get; set; }

        public Product()
        {

        }

        public Product(string name, Money retailPrice, SellByType sellByType = SellByType.Unit)
        {
            _name = name;
            RetailPrice = retailPrice;
            SellByType = sellByType;
        }
    }
}