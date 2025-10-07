using AbstractTasksLogic.Interfaces;

using AutoMapper;
using Contracts;
using Domain.Models;
using MassTransit;

namespace AbstractTasksLogic.Consumers;

public class PriceCrudConsumer : BasicCrudConsumer<object>
{
    public PriceCrudConsumer(
        IMapper mapper,
        ILogger<PriceCrudConsumer> logger, 
        ITypeService typeService, 
        INomenclatureService nomenclatureService, 
        IPriceService priceService, 
        IRemnantService remnantService, 
        IStockService stockService) 
        : base(mapper, logger, typeService, nomenclatureService, priceService, remnantService, stockService)
    {
    }

    public override async Task Consume(ConsumeContext<object> context)
    {
        await base.Consume(context);
        
        try
        {
            switch (context.Message)
            {
                case UpsertCommand<PriceModel> upsertCommand:
                    var upsertedPrice = await _priceService.UpsertPriceAsync(upsertCommand.Data);
                    await context.RespondAsync(upsertedPrice);
                    break;
                    
                case BatchUpsertCommand<PriceModel> batchUpsertCommand:
                    await _priceService.BatchUpsertPricesAsync(batchUpsertCommand.Data);
                    await context.RespondAsync(new { Success = true, Message = "Batch upsert completed" });
                    break;
                    
                case DeleteCommand<PriceModel> deleteCommand:
                    await _priceService.DeletePriceAsync(deleteCommand.Id);
                    await context.RespondAsync(new { Success = true, Message = "Price deleted successfully" });
                    break;
                    
                case BatchDeleteCommand<PriceModel> batchDeleteCommand:
                    await _priceService.BatchDeletePricesAsync(batchDeleteCommand.Ids);
                    await context.RespondAsync(new { Success = true, Message = "Batch delete completed" });
                    break;
                    
                default:
                    await context.RespondAsync(new { Success = false, Message = "Unknown message type" });
                    break;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error processing price operation");
            await context.RespondAsync(new { Success = false, Message = e.Message });
        }
    }
}
