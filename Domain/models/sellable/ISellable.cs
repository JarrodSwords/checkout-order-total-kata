using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
    public interface ISellable
    {
        Money GetSalePrice();
    }
}
