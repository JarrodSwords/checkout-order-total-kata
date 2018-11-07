using System.Collections.Generic;
using NodaMoney;

namespace PillarTechnology.GroceryPointOfSale.Domain
{
    public class SpecialLineItem : LineItem
    {
        public override string Description { get { return $"{ProductName} - special - {SpecialDescription}"; }}
        public IEnumerable<int> LineItemIds { get; }
        public string SpecialDescription { get; }

        public SpecialLineItem(string productName, Money salePrice, IEnumerable<int> lineItemIds, string specialDescription) : base(productName, salePrice)
        {
            LineItemIds = lineItemIds;
            SpecialDescription = specialDescription;
        }
    }
}
