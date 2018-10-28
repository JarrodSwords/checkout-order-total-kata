namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class Item : IScannable
    {
        protected Product _product;

        public int Id { get; set; }

        public Product Product { get { return _product; } }

        public Item(Product product)
        {
            _product = product;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Product: {Product.Name}";
        }
    }
}