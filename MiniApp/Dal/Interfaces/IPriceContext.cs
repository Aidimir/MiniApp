using Dal.Entities;

namespace AbstractTasksDal.Interfaces;

public interface IPriceContext
{
    public Task<PriceEntity> AddPriceAsync(PriceEntity price);
    public Task RemovePriceAsync(Guid id, Guid stockId);
    public Task<PriceEntity> UpdatePriceAsync(PriceEntity price);
    public Task<PriceEntity> GetPriceByIdAsync(Guid id, Guid stockId);
    public Task<List<PriceEntity>> GetPricesByNomenclatureAsync(Guid nomenclatureId);
    public Task<List<PriceEntity>> GetPricesByStockAsync(Guid stockId);
}