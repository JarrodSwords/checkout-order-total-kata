using System;
using NodaMoney;
using UnitsNet;

namespace PointOfSale.Domain
{
    public interface ISellableByMass
    {
        Mass Mass { get; set; }
        Money RetailPricePerUnit { get; set; }
    }

    public class SellableByMass : ISellableByMass
    {
        public Mass Mass { get; set; }
        public Money RetailPricePerUnit { get; set; }
    }

    public class NotSellableByMass : ISellableByMass
    {
        public Mass Mass
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        
        public Money RetailPricePerUnit
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
