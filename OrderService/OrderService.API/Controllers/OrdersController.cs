using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Services.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController(IOrderService _orderService) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            await _orderService.TryPlaceOrderAsync(order);
            return Ok(new { order.Id, Message = "Sipariş işleniyor. Durum bilgisi için bu ID'yi kullanabilirsiniz." });
        }

        [HttpGet("{id}/status")]
        public async Task<IActionResult> GetOrderStatus(Guid id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(new { order.Status, order.Message });
        }
    }
}
