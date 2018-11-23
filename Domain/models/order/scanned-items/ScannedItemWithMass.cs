using System;
using UnitsNet;
using UnitsNet.Units;

namespace PointOfSale.Domain
{
    public class ScannedItemWithMass : ScannedItem
    {
        public ScannedItemWithMass(double massAmount, string massUnit, Product product) : base(
            new MassLineItemFactory(
                new Mass(massAmount, (MassUnit) Enum.Parse(typeof(MassUnit), massUnit)),
                product
            ),
            product
        ) { }
    }
}
