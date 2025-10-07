using Dal.Entities;

namespace AbstractTasksDal.Interfaces;

public interface IRemnantContext
{
    public Task<RemnantEntity> AddRemnantAsync(RemnantEntity remnant);
    public Task RemoveRemnantAsync(Guid id, Guid stockId);
    public Task<RemnantEntity> UpdateRemnantAsync(RemnantEntity remnant);
    public Task<RemnantEntity> GetRemnantByIdAsync(Guid id, Guid stockId);
    public Task<List<RemnantEntity>> GetRemnantsByStockAsync(Guid stockId);
}