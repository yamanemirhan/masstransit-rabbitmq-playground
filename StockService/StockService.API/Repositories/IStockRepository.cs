using StockService.API.Entities;

namespace StockService.API.Repositories
{
    public interface IStockRepository
    {
        Task<Stock?> GetStockByProductIdAsync(Guid productId);
        Task UpdateAsync(Stock stock);
        Task AddAsync(Stock stock);
        Task<bool> DecreaseStockAsync(Guid productId, int amount);
    }
}
