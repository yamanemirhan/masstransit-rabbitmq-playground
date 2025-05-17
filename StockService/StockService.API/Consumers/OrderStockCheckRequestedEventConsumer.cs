using MassTransit;
using Shared.Contracts.Events;
using StockService.API.Repositories;

namespace StockService.API.Consumers
{
    public class OrderStockCheckRequestedEventConsumer(HttpClient _httpClient) : IConsumer<OrderStockCheckRequestedEvent>
    {
        public async Task Consume(ConsumeContext<OrderStockCheckRequestedEvent> context)
        {
            var msg = context.Message;

            var response = await _httpClient.PostAsJsonAsync("http://localhost:5136/api/stocks/decrease-stock", new
            {
                msg.ProductId,
                msg.Quantity
            });

            if (response.IsSuccessStatusCode)
            {
                await context.Publish(new OrderStockConfirmedEvent
                {
                    OrderId = msg.OrderId,
                    ProductId = msg.ProductId,
                    Quantity = msg.Quantity,
                    CustomerEmail = msg.CustomerEmail,
                    CompanyEmail = msg.CompanyEmail,
                    CustomerName = msg.CustomerName,
                    ProductDetails = msg.ProductDetails
                });
            }
            else
            {
                await context.Publish(new OrderStockCheckFailedEvent
                {
                    OrderId = msg.OrderId,
                    Reason = "Stok yetersiz",
                    ProductId = msg.ProductId,
                    CompanyEmail = msg.CompanyEmail,
                    ProductDetails = msg.ProductDetails
                });
            }
        }
    }
}
