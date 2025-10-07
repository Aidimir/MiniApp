using AbstractTasksDal.Interfaces;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AbstractTasksDal.Contexts;

public class RemnantContext : DbContext, IRemnantContext
{
    public RemnantContext(DbContextOptions<RemnantContext> options) : base(options)
    {
    }

    private DbSet<RemnantEntity> _remnants { get; set; }

    public async Task<RemnantEntity> AddRemnantAsync(RemnantEntity remnant)
    {
        await _remnants.AddAsync(remnant);
        await SaveChangesAsync();
        return remnant;
    }

    public async Task RemoveRemnantAsync(Guid id, Guid stockId)
    {
        var existingRemnant = await GetRemnantByIdAsync(id, stockId);
        _remnants.Remove(existingRemnant);
        await SaveChangesAsync();
    }

    public async Task<RemnantEntity> UpdateRemnantAsync(RemnantEntity remnant)
    {
        _remnants.Update(remnant);
        await SaveChangesAsync();
        return remnant;
    }

    public async Task<RemnantEntity> GetRemnantByIdAsync(Guid id, Guid stockId)
    {
        var remnant = await _remnants
            .Include(r => r.Stock)
            .FirstOrDefaultAsync(r => r.ID == id && r.Stock.IDStock == stockId);
        
        if (remnant is null)
            throw new KeyNotFoundException($"Remnant with nomenclature id {id} and stock id {stockId} not found.");
        return remnant;
    }

    public async Task<List<RemnantEntity>> GetRemnantsByStockAsync(Guid stockId)
    {
        var remnants = await _remnants
            .Where(r => r.Stock.IDStock == stockId)
            .ToListAsync();
        
        if (remnants is null || !remnants.Any())
            throw new KeyNotFoundException($"No remnants found for stock id {stockId}");

        return remnants;
    }
}
