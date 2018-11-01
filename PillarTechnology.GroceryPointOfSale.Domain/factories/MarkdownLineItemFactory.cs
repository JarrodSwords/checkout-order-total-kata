namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class MarkdownLineItemFactory : LineItemFactory
    {
        public IScannable Scannable { get; private set; }

        public MarkdownLineItemFactory() { }

        public MarkdownLineItemFactory(IScannable scannable)
        {
            Scannable = scannable;
        }

        public void Configure(IScannable scannable) => Scannable = scannable;

        public override LineItem CreateLineItem()
        {
            return new LineItem(Scannable.Product.Name + " Markdown", -Scannable.Product.Markdown.AmountOffRetail);
        }
    }
}