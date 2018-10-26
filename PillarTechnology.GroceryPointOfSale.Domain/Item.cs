namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class Item : IPurchasable
    {
        private Product _product;

        public Product Product { get { return _product; } }

        public Item(Product product)
        {
            _product = product;
        }
    }
}