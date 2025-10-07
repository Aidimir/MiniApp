using AbstractTasksLogic.Models;
using Domain.Models;

namespace AbstractTasksLogic.Interfaces;

public interface IPriceService
{
    Task<PriceModel> UpsertPriceAsync(PriceModel model);
    Task DeletePriceAsync(Guid id);
    Task<PriceModel> GetPriceByIdAsync(Guid id);
    Task<IEnumerable<PriceModel>> GetAllPricesAsync();
    Task<IEnumerable<PriceModel>> GetPricesByNomenclatureAsync(Guid nomenclatureId);
    Task<IEnumerable<PriceModel>> GetPricesByStockAsync(Guid stockId);
    Task BatchUpsertPricesAsync(List<PriceModel> models);
    Task BatchDeletePricesAsync(List<Guid> ids);
}