using Dal.Entities;

namespace AbstractTasksDal.Interfaces;

public interface IStockContext
{
    public Task<StockEntity> AddStockAsync(StockEntity stock);
    public Task RemoveStockAsync(Guid id);
    public Task<StockEntity> UpdateStockAsync(StockEntity stock);
    public Task<StockEntity> GetStockByIdAsync(Guid id);
    public Task<List<StockEntity>> GetAllStocksAsync();
    public Task<List<StockEntity>> GetStocksByCityAsync(string city);
}