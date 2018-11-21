using System;
using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
    public class SellableAsEaches : Sellable
    {
        public override Mass Mass
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public SellableAsEaches(Money retailPrice) : base(retailPrice) { }

        public override Money GetSalePrice() => RetailPrice;

        public override Money GetSalePrice(Mass mass) =>
            throw new NotImplementedException();
    }
}
