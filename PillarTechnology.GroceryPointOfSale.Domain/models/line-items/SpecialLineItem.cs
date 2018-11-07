using System.Collections.Generic;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class SpecialLineItem : LineItem
    {
        public IEnumerable<int> LineItemIds { get; set; }
        public SpecialLineItem(string description, Money salePrice, IEnumerable<int> lineItemIds) : base(description, salePrice)
        {
            LineItemIds = lineItemIds;
        }
    }
}
