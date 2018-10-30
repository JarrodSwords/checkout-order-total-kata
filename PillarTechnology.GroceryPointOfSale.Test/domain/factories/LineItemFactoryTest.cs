using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public abstract class LineItemFactoryTest
    {
        [Fact]
        public abstract void CreateLineItem_ReturnsLineItem();
    }
}