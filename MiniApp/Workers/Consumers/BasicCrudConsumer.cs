using AbstractTasksLogic.Interfaces;
using AbstractTasksLogic.Services;
using AutoMapper;
using MassTransit;

namespace AbstractTasksLogic.Consumers;

public abstract class BasicCrudConsumer<T> : IConsumer<T> where T : class
{
    protected readonly ITypeService _typeService;
    protected readonly INomenclatureService _nomenclatureService;
    protected readonly IPriceService _priceService;
    protected readonly IRemnantService _remnantService;
    protected readonly  IStockService _stockService;


    protected readonly ILogger<BasicCrudConsumer<T>> _logger;
    protected readonly IMapper _mapper;

    protected BasicCrudConsumer(IMapper mapper,
        ILogger<BasicCrudConsumer<T>> logger, ITypeService typeService, INomenclatureService nomenclatureService, IPriceService priceService, IRemnantService remnantService, IStockService stockService)
    {
        _mapper = mapper;
        _logger = logger;
        _typeService = typeService;
        _nomenclatureService = nomenclatureService;
        _priceService = priceService;
        _remnantService = remnantService;
        _stockService = stockService;
    }

    public virtual async Task Consume(ConsumeContext<T> context)
    {
        _logger.LogInformation("Consuming message: {@Message}", context.Message);
    }
}