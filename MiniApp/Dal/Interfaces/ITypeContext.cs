using Dal.Entities;

namespace AbstractTasksDal.Interfaces;

public interface ITypeContext
{
    public Task<TypeEntity> AddTypeAsync(TypeEntity type);
    public Task RemoveTypeAsync(Guid id);
    public Task<TypeEntity> UpdateTypeAsync(TypeEntity type);
    public Task<TypeEntity> GetTypeByIdAsync(Guid id);
    public Task<List<TypeEntity>> GetAllTypesAsync();
}