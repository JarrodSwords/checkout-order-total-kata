using System;
using UnitsNet;
using UnitsNet.Units;

namespace PointOfSale.Domain
{
    public class MassProduct : Product
    {
        public MassProduct(string name, decimal retailPricePerUnit) : base(
            name,
            new NotSellableAsEaches(),
            new SellableByMass()
        )
        {
            Mass = new Mass(1, MassUnit.Pound);
            RetailPricePerUnit = retailPricePerUnit;
        }

        public MassProduct(string name, decimal retailPricePerUnit, double massAmount, string massUnit) : base(
            name,
            new NotSellableAsEaches(),
            new SellableByMass()
        )
        {
            Mass = new Mass(massAmount, (MassUnit) Enum.Parse(typeof(MassUnit), massUnit));
            RetailPricePerUnit = retailPricePerUnit;
        }

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
