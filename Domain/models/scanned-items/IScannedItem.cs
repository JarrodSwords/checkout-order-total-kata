namespace PointOfSale.Domain
{
    public interface IScannedItem
    {
        MarkdownLineItem CreateMarkdownLineItem();
        RetailLineItem CreateRetailLineItem();
    }
}
