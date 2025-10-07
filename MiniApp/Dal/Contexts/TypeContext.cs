using AbstractTasksDal.Interfaces;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AbstractTasksDal.Contexts;

public class TypeContext : DbContext, ITypeContext
{
    public TypeContext(DbContextOptions<TypeContext> options) : base(options)
    {
    }

    private DbSet<TypeEntity> _types { get; set; }

    public async Task<TypeEntity> AddTypeAsync(TypeEntity type)
    {
        await _types.AddAsync(type);
        await SaveChangesAsync();
        return type;
    }

    public async Task RemoveTypeAsync(Guid id)
    {
        var existingType = await GetTypeByIdAsync(id);
        _types.Remove(existingType);
        await SaveChangesAsync();
    }

    public async Task<TypeEntity> UpdateTypeAsync(TypeEntity type)
    {
        _types.Update(type);
        await SaveChangesAsync();
        return type;
    }

    public async Task<TypeEntity> GetTypeByIdAsync(Guid id)
    {
        var type = await _types.FindAsync(id);
        if (type is null)
            throw new KeyNotFoundException($"Type with id {id} not found.");
        return type;
    }

    public async Task<List<TypeEntity>> GetAllTypesAsync()
    {
        var types = await _types.ToListAsync();
        return types;
    }
}
