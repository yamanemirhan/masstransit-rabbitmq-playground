using MassTransit;
using OrderService.Application.Services.Interfaces;
using OrderService.Domain.Entities;
using OrderService.Domain.Repositories;
using Shared.Contracts.Events;

namespace OrderService.Application.Services
{
    public class OrderAppService(IOrderRepository _orderRepository, IPublishEndpoint _publishEndpoint) : IOrderService
    {
        public async Task<bool> TryPlaceOrderAsync(Order order)
        {
            order.Id = Guid.NewGuid();
            order.Status = "Pending";
            order.Message = "Sipariş işleniyor";

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            var checkStockEvent = new OrderStockCheckRequestedEvent
            {
                OrderId = order.Id,
                ProductId = order.ProductId,
                Quantity = order.Quantity,
                CustomerEmail = order.CustomerEmail,
                CompanyEmail = order.CompanyEmail,
                CustomerName = order.CustomerName,
                ProductDetails = order.ProductDetails
            };

            await _publishEndpoint.Publish(checkStockEvent);
            return true;
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }
    }
}
