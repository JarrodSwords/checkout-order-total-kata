namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class ScannedItem
    {
        protected Product _product;

        public int Id { get; set; }

        public Product Product { get { return _product; } }

        public ScannedItem(Product product)
        {
            _product = product;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Product: {Product.Name}";
        }
    }
}