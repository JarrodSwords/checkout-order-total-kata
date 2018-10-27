using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    /// <summary>
    /// A good available for sale
    /// </summary>
    public class Product
    {
        private string _name;
        private SellByType _sellByType;

        public string Name { get { return _name; } }
        public Money RetailPrice { get; set; }
        public SellByType SellByType { get { return _sellByType; } }

        public Product(string name, Money retailPrice, SellByType sellByType = SellByType.Unit)
        {
            _name = name;
            RetailPrice = retailPrice;
            _sellByType = sellByType;
        }
    }
}