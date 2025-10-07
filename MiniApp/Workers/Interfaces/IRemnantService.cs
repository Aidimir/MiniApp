using AbstractTasksLogic.Models;
using Domain.Models;

namespace AbstractTasksLogic.Interfaces;

public interface IRemnantService
{
    Task<RemnantModel> UpsertRemnantAsync(RemnantModel model);
    Task DeleteRemnantAsync(Guid id);
    Task<RemnantModel> GetRemnantByIdAsync(Guid id);
    Task<IEnumerable<RemnantModel>> GetAllRemnantsAsync();
    Task<IEnumerable<RemnantModel>> GetRemnantsByNomenclatureAsync(Guid nomenclatureId);
    Task<IEnumerable<RemnantModel>> GetRemnantsByStockAsync(Guid stockId);
    Task<IEnumerable<RemnantModel>> GetRemnantsWithStockInfoAsync(Guid nomenclatureId);
    Task BatchUpsertRemnantsAsync(List<RemnantModel> models);
    Task BatchDeleteRemnantsAsync(List<Guid> ids);
}