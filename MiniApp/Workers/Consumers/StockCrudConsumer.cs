using AbstractTasksLogic.Interfaces;
using Domain.Models;
using AutoMapper;
using Contracts;
using MassTransit;

namespace AbstractTasksLogic.Consumers;

public class StockCrudConsumer : BasicCrudConsumer<object>
{
    public StockCrudConsumer(
        IMapper mapper,
        ILogger<StockCrudConsumer> logger, 
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
                case UpsertCommand<StockModel> upsertCommand:
                    var upsertedStock = await _stockService.UpsertStockAsync(upsertCommand.Data);
                    await context.RespondAsync(upsertedStock);
                    break;
                    
                case BatchUpsertCommand<StockModel> batchUpsertCommand:
                    await _stockService.BatchUpsertStocksAsync(batchUpsertCommand.Data);
                    await context.RespondAsync(new { Success = true, Message = "Batch upsert completed" });
                    break;
                    
                case DeleteCommand<StockModel> deleteCommand:
                    await _stockService.DeleteStockAsync(deleteCommand.Id);
                    await context.RespondAsync(new { Success = true, Message = "Stock deleted successfully" });
                    break;
                    
                case BatchDeleteCommand<StockModel> batchDeleteCommand:
                    await _stockService.BatchDeleteStocksAsync(batchDeleteCommand.Ids);
                    await context.RespondAsync(new { Success = true, Message = "Batch delete completed" });
                    break;
                    
                default:
                    await context.RespondAsync(new { Success = false, Message = "Unknown message type" });
                    break;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error processing stock operation");
            await context.RespondAsync(new { Success = false, Message = e.Message });
        }
    }
}
