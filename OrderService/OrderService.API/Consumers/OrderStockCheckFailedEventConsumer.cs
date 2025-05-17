using MassTransit;
using OrderService.Domain.Repositories;
using Shared.Contracts.Events;

namespace OrderService.API.Consumers
{
    public class OrderStockCheckFailedEventConsumer(IOrderRepository _orderRepository) : IConsumer<OrderStockCheckFailedEvent>
    {
        public async Task Consume(ConsumeContext<OrderStockCheckFailedEvent> context)
        {
            var order = await _orderRepository.GetByIdAsync(context.Message.OrderId);
            if (order is not null)
            {
                order.Status = "Failed";
                order.Message = "Stok yetersiz. Sipariş alınamadı.";
                await _orderRepository.UpdateAsync(order);
            }
        }
    }
}
