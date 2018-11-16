using FluentAssertions;
using GroceryPointOfSale.ApplicationServices;
using Xunit;

namespace GroceryPointOfSale.Test
{
    public abstract class IOrderServiceTest
    {
        protected IOrderService _orderService;

        [Fact]
        public void FindOrder_ReturnsOrder()
        {
            var orderId = 1;
            
            var orderDto = _orderService.FindOrder(orderId);

            orderDto.Id.Should().Be(orderId);
        }
    }
}
