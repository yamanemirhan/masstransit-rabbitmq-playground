using EmailService.Worker.Services;
using MassTransit;
using Shared.Contracts.Events;

namespace EmailService.Worker.Consumers
{
    public class OrderStockCheckFailedEventConsumer(IEmailService _emailService) : IConsumer<OrderStockCheckFailedEvent>
    {
        public async Task Consume(ConsumeContext<OrderStockCheckFailedEvent> context)
        {
            var msg = context.Message;
            var body = $"Sipariş alınamadı! ID: {msg.OrderId}, " +
                $"Ürün Detayları: {msg.ProductDetails}, Sebep: {msg.Reason}";

            Console.Write($"Siparis alinamadi: {msg.Reason}");

            await _emailService.SendEmailAsync(msg.CompanyEmail, "Siparis Alinamadi", body);
        }
    }
}
