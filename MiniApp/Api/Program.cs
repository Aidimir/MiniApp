using System.IdentityModel.Tokens.Jwt;
using Contracts.IncomeModels;
using Api;
using Contracts;
using Domain.Models;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using TypeModel = Contracts.TypeModel;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient(); // Регистрируем IHttpClientFactory
builder.Services.AddSingleton<JwtSecurityTokenHandler, CustomJwtTokenHandler>(); // Регистрируем наш кастомный TokenHandler

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMassTransit(x =>
{
    var rabbitMqConfig = builder.Configuration.GetSection("RabbitMQConnection");
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqConfig["Host"], h =>
        {
            h.Username(rabbitMqConfig["Username"] ?? "Guest");
            h.Password(rabbitMqConfig["Password"] ?? "Guest");
        });
        
        cfg.ConfigureEndpoints(context);
    });

    x.AddRequestClient<UpsertCommand<TypeModel>>();
    x.AddRequestClient<BatchUpsertCommand<TypeModel>>();
    x.AddRequestClient<DeleteCommand<TypeModel>>();
    x.AddRequestClient<BatchDeleteCommand<TypeModel>>();

    x.AddRequestClient<UpsertCommand<NomenclatureModel>>();
    x.AddRequestClient<BatchUpsertCommand<NomenclatureModel>>();
    x.AddRequestClient<DeleteCommand<NomenclatureModel>>();
    x.AddRequestClient<BatchDeleteCommand<NomenclatureModel>>();

    x.AddRequestClient<UpsertCommand<PriceModel>>();
    x.AddRequestClient<BatchUpsertCommand<PriceModel>>();
    x.AddRequestClient<DeleteCommand<PriceModel>>();
    x.AddRequestClient<BatchDeleteCommand<PriceModel>>();

    x.AddRequestClient<UpsertCommand<RemnantModel>>();
    x.AddRequestClient<BatchUpsertCommand<RemnantModel>>();
    x.AddRequestClient<DeleteCommand<RemnantModel>>();
    x.AddRequestClient<BatchDeleteCommand<RemnantModel>>();

    x.AddRequestClient<UpsertCommand<StockModel>>();
    x.AddRequestClient<BatchUpsertCommand<StockModel>>();
    x.AddRequestClient<DeleteCommand<StockModel>>();
    x.AddRequestClient<BatchDeleteCommand<StockModel>>();
});
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {jwtSecurityScheme, Array.Empty<string>()}
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var serviceProvider = builder.Services.BuildServiceProvider();
        options.TokenHandlers.Clear();
        options.TokenHandlers.Add(serviceProvider.GetRequiredService<JwtSecurityTokenHandler>());
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();