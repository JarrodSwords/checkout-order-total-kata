namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class Item : IScannable
    {
        private Product _product;

        public Product Product { get { return _product; } }

        public Item(Product product)
        {
            _product = product;
        }
    }
}