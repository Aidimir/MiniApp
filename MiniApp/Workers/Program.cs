using AbstractTasksLogic;
using AbstractTasksLogic.Consumers;
using AbstractTasksLogic.Interfaces;
using AbstractTasksLogic.Services;
using Api;
using MassTransit;
using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("workers_appsettings.json")
    .AddEnvironmentVariables();

// Настройка Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information() // Уровень логирования
    .WriteTo.Console( // Вывод в консоль
        new JsonFormatter())
    .CreateLogger();

// Интеграция Serilog в Microsoft.Extensions.Logging
builder.Host.UseSerilog();

// Регистрация сервисов
builder.Services.AddAutoMapper(typeof(AutoMappingProfile));
builder.Services.AddRepositories(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<ITypeService, TypeService>();
builder.Services.AddScoped<INomenclatureService, NomenclatureService>();
builder.Services.AddScoped<IPriceService, PriceService>();
builder.Services.AddScoped<IRemnantService, RemnantService>();
builder.Services.AddScoped<IStockService, StockService>();


// Регистрация MassTransit только с консюмерами
builder.Services.AddMassTransit(x =>
{
    // Регистрация консюмеров
    x.AddConsumer<NomenclatureCrudConsumer>();
    x.AddConsumer<RemnantCrudConsumer>();
    x.AddConsumer<TypeCrudConsumer>();
    x.AddConsumer<PriceCrudConsumer>();
    x.AddConsumer<StockCrudConsumer>();

    var rabbitMqConfig = builder.Configuration.GetSection("RabbitMQConnection");
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqConfig["Host"], h =>
        {
            h.Username(rabbitMqConfig["Username"] ?? "Guest");
            h.Password(rabbitMqConfig["Password"] ?? "Guest");
        });

        // Явная настройка каждого эндпоинта с ConcurrentMessageLimit
        cfg.ReceiveEndpoint("nomenclature-crud", e =>
        {
            e.PrefetchCount = 10;
            e.ConcurrentMessageLimit = 3;
            e.ConfigureConsumer<NomenclatureCrudConsumer>(context);
        });

        cfg.ReceiveEndpoint("remnant-crud", e =>
        {
            e.PrefetchCount = 10;
            e.ConcurrentMessageLimit = 3;
            e.ConfigureConsumer<RemnantCrudConsumer>(context);
        });

        cfg.ReceiveEndpoint("type-crud", e =>
        {
            e.PrefetchCount = 10;
            e.ConcurrentMessageLimit = 3;
            e.ConfigureConsumer<TypeCrudConsumer>(context);
        });

        cfg.ReceiveEndpoint("price-crud", e =>
        {
            e.PrefetchCount = 10;
            e.ConcurrentMessageLimit = 3;
            e.ConfigureConsumer<PriceCrudConsumer>(context);
        });

        cfg.ReceiveEndpoint("stock-crud", e =>
        {
            e.PrefetchCount = 10;
            e.ConcurrentMessageLimit = 3;
            e.ConfigureConsumer<StockCrudConsumer>(context);
        });
    });
});


var app = builder.Build();

try
{
    Log.Information("Starting the application...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly!");
    throw;
}
finally
{
    Log.CloseAndFlush();
}