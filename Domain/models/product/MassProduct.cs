using System;
using UnitsNet;
using UnitsNet.Units;

namespace PointOfSale.Domain
{
    public class MassProduct : Product
    {
        public Mass Mass { get; set; }

        public MassProduct(string name, decimal retailPrice) : base(name, retailPrice)
        {
            Mass = new Mass(1, MassUnit.Pound);
        }

        public MassProduct(string name, decimal retailPrice, double massAmount, string massUnit) : base(name, retailPrice)
        {
            Mass = new Mass(
                massAmount,
                (MassUnit) Enum.Parse(typeof(MassUnit), massUnit)
            );
        }

        public class MassProductBuilder
        {
            public double MassAmount { get; set; }
            public string MassUnit { get; set; }
            public string Name { get; set; }
            public decimal RetailPrice { get; set; }

            public MassProductBuilder(string name, decimal retailPrice)
            {
                Name = name;
                RetailPrice = retailPrice;
            }

            public MassProductBuilder SetMass(double massAmount, string massUnit)
            {
                MassAmount = massAmount;
                MassUnit = massUnit;
                return this;
            }

            public MassProduct Build()
            {
                return new MassProduct(Name, RetailPrice);
            }
        }
    }
}
