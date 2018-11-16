using AutoMapper;
using GroceryPointOfSale.ApplicationServices;
using GroceryPointOfSale.Domain;

namespace GroceryPointOfSale.ApplicationServiceImplementations
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IMapper mapper, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public OrderDto FindOrder(long orderId)
        {
            var product = _orderRepository.FindOrder(orderId);
            return _mapper.Map<OrderDto>(product);
        }
    }
}
