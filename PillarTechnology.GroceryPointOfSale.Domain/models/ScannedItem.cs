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

        public override string ToString() => $"Id: {Id}, Product: {Product.Name}";

        public virtual LineItem CreateLineItem()
        {
            return new LineItem(Product.Name, Product.RetailPrice, Id);
        }

        public virtual LineItem CreateMarkdownLineItem()
        {
            return new LineItem($"{Product.Name} markdown", -Product.Markdown.AmountOffRetail);
        }
    }
}