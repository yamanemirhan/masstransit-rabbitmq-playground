using OrderService.Domain.Entities;

namespace OrderService.Application.Services.Interfaces
{
    public interface IOrderService
    {
        //Task<Order> CreateOrderAsync(Order order);
        Task<bool> TryPlaceOrderAsync(Order order);
        Task<Order?> GetByIdAsync(Guid id);
    }
}
