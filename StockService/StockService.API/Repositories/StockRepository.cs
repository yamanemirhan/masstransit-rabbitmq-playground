using Microsoft.EntityFrameworkCore;
using StockService.API.Data;
using StockService.API.Entities;

namespace StockService.API.Repositories
{
    public class StockRepository(StockDbContext _context) : IStockRepository
    {
        public async Task<Stock?> GetStockByProductIdAsync(Guid productId)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.ProductId == productId);
        }

        public async Task UpdateAsync(Stock stock)
        {
            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DecreaseStockAsync(Guid productId, int quantity)
        {
            var stock = await _context.Stocks
                .FirstOrDefaultAsync(s => s.ProductId == productId);

            if (stock == null || stock.Quantity < quantity)
            {
                return false;
            }

            stock.Quantity -= quantity;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
