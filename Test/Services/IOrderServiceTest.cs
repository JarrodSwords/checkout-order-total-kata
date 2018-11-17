using FluentAssertions;
using PointOfSale.Services;
using Xunit;

namespace PointOfSale.Test.Services
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
