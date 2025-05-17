using MassTransit;
using OrderService.Domain.Repositories;
using Shared.Contracts.Events;

namespace OrderService.API.Consumers
{
    public class OrderStockConfirmedEventConsumer(IOrderRepository _orderRepository) : IConsumer<OrderStockConfirmedEvent>
    {
        //public async Task Consume(ConsumeContext<OrderStockConfirmedEvent> context)
        //{
        //    var msg = context.Message;

        //    var order = new Order
        //    {
        //        ProductId = msg.ProductId,
        //        Quantity = msg.Quantity,
        //        CustomerEmail = msg.CustomerEmail,
        //        CompanyEmail = msg.CompanyEmail,
        //        CustomerName = msg.CustomerName,
        //        ProductDetails = msg.ProductDetails,
        //        OrderDate = DateTime.UtcNow
        //    };

        //    await _orderRepository.AddAsync(order);
        //    await _orderRepository.SaveChangesAsync();
        //}
        public async Task Consume(ConsumeContext<OrderStockConfirmedEvent> context)
        {
            var order = await _orderRepository.GetByIdAsync(context.Message.OrderId);
            if (order is not null)
            {
                order.Status = "Success";
                order.Message = "Siparişiniz başarıyla alındı.";
                await _orderRepository.UpdateAsync(order);
            }
        }
    }
}
