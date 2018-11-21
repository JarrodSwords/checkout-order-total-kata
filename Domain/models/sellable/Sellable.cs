using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
    public abstract class Sellable : ISellable
    {
        public abstract Mass Mass { get; set; }
        public Money RetailPrice { get; set; }

        public Sellable(Money retailPrice)
        {
            RetailPrice = retailPrice;
        }

        public abstract Money GetSalePrice();

        public abstract Money GetSalePrice(Mass mass);
    }
}
