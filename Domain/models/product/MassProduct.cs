using System;
using UnitsNet;
using UnitsNet.Units;

namespace PointOfSale.Domain
{
    public class MassProduct : Product
    {
        public MassProduct(string name, decimal retailPrice) : base(
            name,
            new MarkdownableWithMass(),
            new SellableByMass(retailPrice)
        ) { }

        public MassProduct(string name, decimal retailPrice, double massAmount, string massUnit) : base(
            name,
            new MarkdownableWithMass(massAmount, massUnit),
            new SellableByMass(retailPrice, massAmount, massUnit)
        ) { }

        public class MassProductBuilder
        {
            public double MassAmount { get; set; }
            public string MassUnit { get; set; }
            public string Name { get; set; }
            public decimal RetailPricePerUnit { get; set; }

            public MassProductBuilder(string name, decimal retailPricePerUnit)
            {
                Name = name;
                RetailPricePerUnit = retailPricePerUnit;
            }

            public MassProductBuilder SetMass(double massAmount, string massUnit)
            {
                MassAmount = massAmount;
                MassUnit = massUnit;
                return this;
            }

            public MassProduct Build()
            {
                return new MassProduct(Name, RetailPricePerUnit);
            }
        }
    }
}
