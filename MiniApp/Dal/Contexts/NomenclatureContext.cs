using AbstractTasksDal.Interfaces;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AbstractTasksDal.Contexts;

public class NomenclatureContext : DbContext, INomenclatureContext
{
    public NomenclatureContext(DbContextOptions<NomenclatureContext> options) : base(options)
    {
    }

    private DbSet<NomenclatureEntity> _nomenclatures { get; set; }

    public async Task<NomenclatureEntity> AddNomenclatureAsync(NomenclatureEntity nomenclature)
    {
        await _nomenclatures.AddAsync(nomenclature);
        await SaveChangesAsync();
        return nomenclature;
    }

    public async Task RemoveNomenclatureAsync(string id)
    {
        var existingNomenclature = await GetNomenclatureByIdAsync(id);
        _nomenclatures.Remove(existingNomenclature);
        await SaveChangesAsync();
    }

    public async Task<NomenclatureEntity> UpdateNomenclatureAsync(NomenclatureEntity nomenclature)
    {
        _nomenclatures.Update(nomenclature);
        await SaveChangesAsync();
        return nomenclature;
    }

    public async Task<NomenclatureEntity> GetNomenclatureByIdAsync(string id)
    {
        var nomenclature = await _nomenclatures
            .Include(n => n.Type)
            .FirstOrDefaultAsync(n => n.ID == id);
        
        if (nomenclature is null)
            throw new KeyNotFoundException($"Nomenclature with id {id} not found.");
        return nomenclature;
    }

    public async Task<List<NomenclatureEntity>> GetAllNomenclaturesAsync()
    {
        var nomenclatures = await _nomenclatures
            .Include(n => n.Type)
            .ToListAsync();
        return nomenclatures;
    }
}
