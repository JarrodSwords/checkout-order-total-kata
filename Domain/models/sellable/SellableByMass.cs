using System;
using NodaMoney;
using UnitsNet;
using UnitsNet.Units;

namespace PointOfSale.Domain
{
    public class SellableByMass : Sellable
    {
        public override Mass Mass { get; set; }

        public SellableByMass(Money retailPrice) : base(retailPrice)
        {
            Mass = new Mass(1, MassUnit.Pound);
        }

        public SellableByMass(Money retailPrice, double massAmount, string massUnit) : base(retailPrice)
        {
            Mass = new Mass(
                massAmount,
                (MassUnit) Enum.Parse(typeof(MassUnit), massUnit)
            );
        }

        public override Money GetSalePrice() =>
            throw new NotImplementedException();

        public override Money GetSalePrice(Mass mass) =>
            RetailPrice * (decimal) (mass / Mass);
    }
}
