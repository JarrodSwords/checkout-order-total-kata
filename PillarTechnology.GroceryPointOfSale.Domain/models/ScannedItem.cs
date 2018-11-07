using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class ScannedItem
    {
        public int Id { get; set; }
        public virtual Money MarkdownDiscount { get { return -Product.Markdown.AmountOffRetail; } }
        public Product Product { get; }
        public virtual Money RetailPrice { get { return Product.RetailPrice; } }

        public ScannedItem(Product product)
        {
            Product = product;
        }

        public virtual LineItem CreateMarkdownLineItem()
        {
            return new MarkdownLineItem(Product.Name, MarkdownDiscount, Id);
        }

        public LineItem CreateRetailLineItem()
        {
            return new RetailLineItem(Product.Name, RetailPrice, Id);
        }

        public override string ToString() => $"Id: {Id}, Product: {Product.Name}";
    }
}
