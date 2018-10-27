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
        public SellByType SellByType { get { return _sellByType; } }

        public Product(string name, SellByType sellByType = SellByType.Unit)
        {
            _name = name;
            _sellByType = sellByType;
        }
    }
}