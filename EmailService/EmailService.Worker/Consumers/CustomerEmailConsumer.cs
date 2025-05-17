using EmailService.Worker.Services;
using MassTransit;
using Shared.Contracts.Events;

namespace EmailService.Worker.Consumers
{
    public class CustomerEmailConsumer(IEmailService _emailService) : IConsumer<OrderStockConfirmedEvent>
    {
        public async Task Consume(ConsumeContext<OrderStockConfirmedEvent> context)
        {
            var msg = context.Message;
            var body = $"Merhaba {msg.CustomerName}, siparişiniz alındı. Detaylar: {msg.ProductDetails}";
            Console.WriteLine("Siparis alindi customer");
            await _emailService.SendEmailAsync(msg.CustomerEmail, "Sipariş Onayı", body);
        }
    }
}
