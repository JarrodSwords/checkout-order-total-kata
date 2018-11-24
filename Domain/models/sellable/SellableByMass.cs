using System;
using NodaMoney;
using UnitsNet;
using UnitsNet.Units;

namespace PointOfSale.Domain
{
    public class SellableByMass : ISellable
    {
        public Mass Mass { get; }
        public MassProduct MassProduct { get; }

        public SellableByMass(Mass mass, MassProduct massProduct)
        {
            Mass = mass;
            MassProduct = massProduct;
        }

        public Money GetSalePrice() => MassProduct.RetailPrice * (decimal) (Mass / MassProduct.Mass);
    }
}
