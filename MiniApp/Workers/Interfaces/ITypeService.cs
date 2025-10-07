using AbstractTasksLogic.Models;
using Domain.Models;

namespace AbstractTasksLogic.Interfaces;

public interface ITypeService
{
    Task<TypeModel> UpsertTypeAsync(TypeModel model);
    Task DeleteTypeAsync(Guid id);
    Task<TypeModel> GetTypeByIdAsync(Guid id);
    Task<IEnumerable<TypeModel>> GetAllTypesAsync();
    Task BatchUpsertTypesAsync(List<TypeModel> models);
    Task BatchDeleteTypesAsync(List<Guid> ids);
}