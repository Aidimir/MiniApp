using AbstractTasksDal;
using AbstractTasksDal.Contexts;
using AbstractTasksDal.Interfaces;
using AbstractTasksLogic.Interfaces;
using AbstractTasksLogic.Services;
using Microsoft.EntityFrameworkCore;

namespace Api;

public static class AddRepositoriesExtension
{
public static void AddRepositories(this IServiceCollection services, string? connectionString)
{
    // Регистрируем все контексты
    services.AddDbContext<INomenclatureContext, NomenclatureContext>(options => 
    { 
        options.UseNpgsql(connectionString); 
    });
    
    services.AddDbContext<IPriceContext, PriceContext>(options => 
    { 
        options.UseNpgsql(connectionString); 
    });
    
    services.AddDbContext<IRemnantContext, RemnantContext>(options => 
    { 
        options.UseNpgsql(connectionString); 
    });
    
    services.AddDbContext<IStockContext, StockContext>(options => 
    { 
        options.UseNpgsql(connectionString); 
    });
    
    services.AddDbContext<ITypeContext, TypeContext>(options => 
    { 
        options.UseNpgsql(connectionString); 
    });

    // Регистрируем сервисы
    services.AddScoped<ITypeService, TypeService>();
    services.AddScoped<INomenclatureService, NomenclatureService>();
    services.AddScoped<IPriceService, PriceService>();
    services.AddScoped<IRemnantService, RemnantService>();
    services.AddScoped<IStockService, StockService>();

    // Применяем миграции для каждого контекста
    using (var provider = services.BuildServiceProvider())
    {
        // Для NomenclatureContext
        var nomenclatureContext = provider.GetRequiredService<NomenclatureContext>();
        if (nomenclatureContext.Database.GetPendingMigrations().Any())
            nomenclatureContext.Database.Migrate();

        // Для PriceContext
        var priceContext = provider.GetRequiredService<PriceContext>();
        if (priceContext.Database.GetPendingMigrations().Any())
            priceContext.Database.Migrate();

        // Для RemnantContext
        var remnantContext = provider.GetRequiredService<RemnantContext>();
        if (remnantContext.Database.GetPendingMigrations().Any())
            remnantContext.Database.Migrate();

        // Для StockContext
        var stockContext = provider.GetRequiredService<StockContext>();
        if (stockContext.Database.GetPendingMigrations().Any())
            stockContext.Database.Migrate();

        // Для TypeContext
        var typeContext = provider.GetRequiredService<TypeContext>();
        if (typeContext.Database.GetPendingMigrations().Any())
            typeContext.Database.Migrate();
    }
}
}