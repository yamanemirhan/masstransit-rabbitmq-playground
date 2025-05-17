using EmailService.Worker.Services;
using MassTransit;
using Shared.Contracts.Events;

namespace EmailService.Worker.Consumers
{
    public class CompanyEmailConsumer(IEmailService _emailService) : IConsumer<OrderStockConfirmedEvent>
    {
        public async Task Consume(ConsumeContext<OrderStockConfirmedEvent> context)
        {
            var msg = context.Message;
            var body = $"Yeni sipariş alındı! ID: {msg.OrderId}, Ürün Detayları: {msg.ProductDetails}";
            Console.WriteLine("Siparis alindi company.");
            await _emailService.SendEmailAsync(msg.CompanyEmail, "Yeni Sipariş", body);
        }
    }
}
