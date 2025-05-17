using Microsoft.AspNetCore.Mvc;
using StockService.API.DTOs.Requests;
using StockService.API.Entities;
using StockService.API.Repositories;

namespace StockService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StocksController(IStockRepository _stockRepository) : ControllerBase
    {
        [HttpPost("decrease-stock")]
        public async Task<IActionResult> DecreaseStock([FromBody] DecreaseStockRequest request)
        {
            var success = await _stockRepository.DecreaseStockAsync(request.ProductId, request.Quantity);
            if (success)
            {
                return Ok("Stok başarıyla güncellendi.");
            }
            return BadRequest("Stok güncellenemedi.");
        }

        [HttpPost("add-stock")]
        public async Task<IActionResult> AddStock([FromBody] AddStockRequest request)
        {
            try
            {
                var existing = await _stockRepository.GetStockByProductIdAsync(request.ProductId);
                if (existing != null)
                {
                    return Conflict("Bu ürüne ait stok zaten mevcut.");
                }
                var stock = new Stock
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                };

                await _stockRepository.AddAsync(stock);

                return Ok("Stok başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Sunucu hatası.");
            }
        }
    }
}
