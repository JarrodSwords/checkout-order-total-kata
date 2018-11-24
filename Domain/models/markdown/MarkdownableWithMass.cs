using System;
using NodaMoney;
using UnitsNet;
using UnitsNet.Units;

namespace PointOfSale.Domain
{
    public class MarkdownableWithMass : IMarkdownable
    {
        public Mass Mass { get; set; }
        public MassProduct MassProduct { get; set; }

        public MarkdownableWithMass(MassProduct massProduct)
        {
            Mass = new Mass(1, MassUnit.Pound);
            MassProduct = massProduct;
        }

        public MarkdownableWithMass(MassProduct massProduct, double massAmount, string massUnit)
        {
            Mass = new Mass(
                massAmount,
                (MassUnit) Enum.Parse(typeof(MassUnit), massUnit)
            );
            MassProduct = massProduct;
        }

        public Money GetMarkdownSalePrice() =>
            -MassProduct.Markdown.AmountOffRetail * (decimal) (Mass / MassProduct.Mass);
    }
}
