namespace PillarTechnology.GroceryPointOfSale.Domain
{
    /// <summary>
    /// A good available for sale
    /// </summary>
    public class Product
    {
        private string _name;

        public string Name { get { return _name; } }

        public Product(string name)
        {
            _name = name;
        }

    }
}