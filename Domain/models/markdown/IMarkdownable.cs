using NodaMoney;

namespace PointOfSale.Domain
{
    public interface IMarkdownable
    {
        Money GetMarkdownSalePrice();
    }
}
