using AbstractTasksLogic.Models;
using Domain.Models;

namespace AbstractTasksLogic.Interfaces;

public interface INomenclatureService
{
    Task<NomenclatureModel> UpsertNomenclatureAsync(NomenclatureModel model);
    Task DeleteNomenclatureAsync(Guid id);
    Task<NomenclatureModel> GetNomenclatureByIdAsync(Guid id);
    Task<IEnumerable<NomenclatureModel>> GetAllNomenclaturesAsync();
    Task<IEnumerable<NomenclatureModel>> GetNomenclaturesByTypeAsync(Guid typeId);
    Task BatchUpsertNomenclaturesAsync(List<NomenclatureModel> models);
    Task BatchDeleteNomenclaturesAsync(List<Guid> ids);
}