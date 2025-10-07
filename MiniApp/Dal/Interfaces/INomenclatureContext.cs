using Dal.Entities;

namespace AbstractTasksDal.Interfaces;

public interface INomenclatureContext
{
    public Task<NomenclatureEntity> AddNomenclatureAsync(NomenclatureEntity nomenclature);
    public Task RemoveNomenclatureAsync(string id);
    public Task<NomenclatureEntity> UpdateNomenclatureAsync(NomenclatureEntity nomenclature);
    public Task<NomenclatureEntity> GetNomenclatureByIdAsync(string id);
    public Task<List<NomenclatureEntity>> GetAllNomenclaturesAsync();
}