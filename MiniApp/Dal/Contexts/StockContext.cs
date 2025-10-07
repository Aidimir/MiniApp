using AbstractTasksDal.Interfaces;
using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace AbstractTasksDal.Contexts;

public class StockContext : DbContext, IStockContext
{
    public StockContext(DbContextOptions<StockContext> options) : base(options)
    {
    }

    private DbSet<StockEntity> _stocks { get; set; }

    public async Task<StockEntity> AddStockAsync(StockEntity stock)
    {
        await _stocks.AddAsync(stock);
        await SaveChangesAsync();
        return stock;
    }

    public async Task RemoveStockAsync(Guid id)
    {
        var existingStock = await GetStockByIdAsync(id);
        _stocks.Remove(existingStock);
        await SaveChangesAsync();
    }

    public async Task<StockEntity> UpdateStockAsync(StockEntity stock)
    {
        _stocks.Update(stock);
        await SaveChangesAsync();
        return stock;
    }

    public async Task<StockEntity> GetStockByIdAsync(Guid id)
    {
        var stock = await _stocks.FindAsync(id);
        if (stock is null)
            throw new KeyNotFoundException($"Stock with id {id} not found.");
        return stock;
    }

    public async Task<List<StockEntity>> GetAllStocksAsync()
    {
        var stocks = await _stocks.ToListAsync();
        return stocks;
    }

    public async Task<List<StockEntity>> GetStocksByCityAsync(string city)
    {
        var stocks = await _stocks
            .Where(s => s.Stock.Contains(city))
            .ToListAsync();
        
        if (stocks is null || !stocks.Any())
            throw new KeyNotFoundException($"No stocks found in city {city}");

        return stocks;
    }
}
