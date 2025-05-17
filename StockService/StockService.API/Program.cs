using MassTransit;
using Microsoft.EntityFrameworkCore;
using StockService.API.Consumers;
using StockService.API.Data;
using StockService.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StockDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("StockDb")));

builder.Services.AddHttpClient();

builder.Services.AddScoped<IStockRepository, StockRepository>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderStockCheckRequestedEventConsumer>();

    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["MassTransit:RabbitMq:Host"], h =>
        {
            h.Username(builder.Configuration["MassTransit:RabbitMq:Username"]);
            h.Password(builder.Configuration["MassTransit:RabbitMq:Password"]);
        });

        cfg.ReceiveEndpoint("order-stock-check-requested-queue", e =>
        {
            e.ConfigureConsumer<OrderStockCheckRequestedEventConsumer>(ctx);

            e.PrefetchCount = 1;
        });
    });
});


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
