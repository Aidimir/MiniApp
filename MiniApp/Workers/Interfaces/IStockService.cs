using AbstractTasksLogic.Models;
using Domain.Models;

namespace AbstractTasksLogic.Interfaces;

public interface IStockService
{
    Task<StockModel> UpsertStockAsync(StockModel model);
    Task DeleteStockAsync(Guid id);
    Task<StockModel> GetStockByIdAsync(Guid id);
    Task<IEnumerable<StockModel>> GetAllStocksAsync();
    Task<IEnumerable<StockModel>> GetStocksByCityAsync(string city);
    Task BatchUpsertStocksAsync(List<StockModel> models);
    Task BatchDeleteStocksAsync(List<Guid> ids);
}