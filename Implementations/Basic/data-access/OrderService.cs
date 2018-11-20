using AutoMapper;
using PointOfSale.Domain;
using PointOfSale.Services;

namespace PointOfSale.Implementations.Basic
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
            var order = _orderRepository.FindOrder(orderId);
            return _mapper.Map<OrderDto>(order);
        }

        public InvoiceDto GetInvoice(long orderId)
        {
            var order = _orderRepository.FindOrder(orderId);
            var invoice = new Invoice.Factory(order).CreateInvoice();
            return _mapper.Map<InvoiceDto>(invoice);
        }
    }
}
