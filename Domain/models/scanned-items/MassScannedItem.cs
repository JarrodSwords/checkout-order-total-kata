using System;
using UnitsNet;
using UnitsNet.Units;

namespace PointOfSale.Domain
{
    public class MassScannedItem : ScannedItem
    {
        public Mass Mass
        {
            get => ((SellableByMass) _sellable).Mass;
        }

        public MassScannedItem(double massAmount, string massUnit, MassProduct product) : base(
            new MarkdownableWithMass(
                product,
                massAmount,
                massUnit
            ),
            product,
            new SellableByMass(
                new Mass(
                    massAmount,
                    (MassUnit) Enum.Parse(typeof(MassUnit), massUnit)
                ),
                product
            )
        ) { }
    }
}
