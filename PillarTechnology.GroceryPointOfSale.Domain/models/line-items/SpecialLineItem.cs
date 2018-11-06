using System.Collections.Generic;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class SpecialLineItem : LineItem
    {
        public SpecialLineItem(string description, Money salePrice) : base(description, salePrice) { }
    }
}
