using AbstractTasksLogic.Interfaces;
using Contracts.IncomeModels;
using Contracts.OutcomeModels;
using AbstractTasksLogic.Services;
using AutoMapper;
using Contracts;
using Domain.Models;
using MassTransit;

namespace AbstractTasksLogic.Consumers;

public class RemnantCrudConsumer : BasicCrudConsumer<object>
{
    public RemnantCrudConsumer(
        IMapper mapper,
        ILogger<RemnantCrudConsumer> logger, 
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
                case UpsertCommand<RemnantModel> upsertCommand:
                    var upsertedRemnant = await _remnantService.UpsertRemnantAsync(upsertCommand.Data);
                    await context.RespondAsync(upsertedRemnant);
                    break;
                    
                case BatchUpsertCommand<RemnantModel> batchUpsertCommand:
                    await _remnantService.BatchUpsertRemnantsAsync(batchUpsertCommand.Data);
                    await context.RespondAsync(new { Success = true, Message = "Batch upsert completed" });
                    break;
                    
                case DeleteCommand<RemnantModel> deleteCommand:
                    await _remnantService.DeleteRemnantAsync(deleteCommand.Id);
                    await context.RespondAsync(new { Success = true, Message = "Remnant deleted successfully" });
                    break;
                    
                case BatchDeleteCommand<RemnantModel> batchDeleteCommand:
                    await _remnantService.BatchDeleteRemnantsAsync(batchDeleteCommand.Ids);
                    await context.RespondAsync(new { Success = true, Message = "Batch delete completed" });
                    break;
                    
                default:
                    await context.RespondAsync(new { Success = false, Message = "Unknown message type" });
                    break;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error processing remnant operation");
            await context.RespondAsync(new { Success = false, Message = e.Message });
        }
    }
}
