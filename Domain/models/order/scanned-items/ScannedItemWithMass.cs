using System;
using UnitsNet;
using UnitsNet.Units;

namespace PointOfSale.Domain
{
    public class ScannedItemWithMass : ScannedItem
    {
        public ScannedItemWithMass(decimal massAmount, string massUnit, Product product) : base(
            new MassLineItemFactory(
                new Mass((double) massAmount, (MassUnit) Enum.Parse(typeof(MassUnit), massUnit)),
                product
            ),
            product
        ) { }
    }
}
