
using EmailService.Worker;
using EmailService.Worker.Consumers;
using EmailService.Worker.Services;
using EmailService.Worker.Settings;
using MassTransit;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        var config = builder.Configuration;

        builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));
        builder.Services.AddScoped<IEmailService, EmailAppService>();

        builder.Services.AddMassTransit(x =>
        {
            x.AddConsumer<CompanyEmailConsumer>();
            x.AddConsumer<CustomerEmailConsumer>();
            x.AddConsumer<OrderStockCheckFailedEventConsumer>();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(config["MassTransit:RabbitMq:Host"], h =>
                {
                    h.Username(config["MassTransit:RabbitMq:Username"]);
                    h.Password(config["MassTransit:RabbitMq:Password"]);
                });

                cfg.ReceiveEndpoint("company-email-queue", e =>
                {
                    e.ConfigureConsumer<CompanyEmailConsumer>(ctx);
                });

                cfg.ReceiveEndpoint("customer-email-queue", e =>
                {
                    e.ConfigureConsumer<CustomerEmailConsumer>(ctx);
                });

                cfg.ReceiveEndpoint("order-stock-check-failed-queue", e =>
                {
                    e.ConfigureConsumer<OrderStockCheckFailedEventConsumer>(ctx);
                });
            });
        });

        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();
        host.Run();
    }
}