using AbstractTasksDal.Interfaces;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AbstractTasksDal.Contexts;

public class PriceContext : DbContext, IPriceContext
{
    public PriceContext(DbContextOptions<PriceContext> options) : base(options)
    {
    }

    private DbSet<PriceEntity> _prices { get; set; }

    public async Task<PriceEntity> AddPriceAsync(PriceEntity price)
    {
        await _prices.AddAsync(price);
        await SaveChangesAsync();
        return price;
    }

    public async Task RemovePriceAsync(Guid id, Guid stockId)
    {
        var existingPrice = await GetPriceByIdAsync(id, stockId);
        _prices.Remove(existingPrice);
        await SaveChangesAsync();
    }

    public async Task<PriceEntity> UpdatePriceAsync(PriceEntity price)
    {
        _prices.Update(price);
        await SaveChangesAsync();
        return price;
    }

    public async Task<PriceEntity> GetPriceByIdAsync(Guid id, Guid stockId)
    {
        var price = await _prices
            .Include(p => p.Stock)
            .FirstOrDefaultAsync(p => p.ID == id && p.Stock.IDStock == stockId);
        
        if (price is null)
            throw new KeyNotFoundException($"Price with nomenclature id {id} and stock id {stockId} not found.");
        return price;
    }

    public async Task<List<PriceEntity>> GetPricesByNomenclatureAsync(Guid nomenclatureId)
    {
        var prices = await _prices
            .Where(p => p.ID == nomenclatureId)
            .Include(p => p.Stock)
            .ToListAsync();
        
        if (prices is null || !prices.Any())
            throw new KeyNotFoundException($"No prices found for nomenclature id {nomenclatureId}");

        return prices;
    }

    public async Task<List<PriceEntity>> GetPricesByStockAsync(Guid stockId)
    {
        var prices = await _prices
            .Where(p => p.Stock.IDStock == stockId)
            .ToListAsync();
        
        if (prices is null || !prices.Any())
            throw new KeyNotFoundException($"No prices found for stock id {stockId}");

        return prices;
    }
}
