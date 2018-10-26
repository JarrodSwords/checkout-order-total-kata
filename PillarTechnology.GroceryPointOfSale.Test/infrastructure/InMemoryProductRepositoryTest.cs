using FluentAssertions;
using Moq;
using PillarTechnology.GroceryPointOfSale.Domain;
using PillarTechnology.GroceryPointOfSale.Infrastructure.InMemory;
using Xunit;

namespace PillarTechnology.GroceryPointOfSale.Test
{
    public class InMemoryProductRepositoryTest : IProductRepositoryTest
    {
        public InMemoryProductRepositoryTest()
        {
            _productRepository = new InMemoryProductRepository();
        }
    }
}