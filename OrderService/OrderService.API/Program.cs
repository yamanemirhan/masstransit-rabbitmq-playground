using Microsoft.EntityFrameworkCore;
using OrderService.Application.Services.Interfaces;
using OrderService.Domain.Repositories;
using OrderService.Application.Services;
using OrderService.Infrastructure.Contexts;
using OrderService.Infrastructure.Repositories;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderAppService>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(typeof(Program).Assembly);

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["MassTransit:RabbitMq:Host"], h =>
        {
            h.Username(builder.Configuration["MassTransit:RabbitMq:Username"]);
            h.Password(builder.Configuration["MassTransit:RabbitMq:Password"]);
        });

        cfg.ConfigureEndpoints(context);

    });
});


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
