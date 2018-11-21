using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
    public interface ISellable
    {
        Mass Mass { get; set; }
        Money RetailPrice { get; set; }
        Money GetSalePrice();
        Money GetSalePrice(Mass mass);
    }
}
