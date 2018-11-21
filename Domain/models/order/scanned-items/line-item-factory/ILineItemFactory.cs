using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
    public interface ILineItemFactory
    {
        int Id { get; set; }
        Product Product { get; }
        Mass Mass { get; }
        Money SalePrice { get; }

        MarkdownLineItem CreateMarkdownLineItem();
        RetailLineItem CreateRetailLineItem();
    }
}
