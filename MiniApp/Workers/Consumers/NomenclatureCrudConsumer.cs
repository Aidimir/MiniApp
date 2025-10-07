using AbstractTasksLogic.Interfaces;
using AbstractTasksLogic.Models;
using Contracts.IncomeModels;
using Contracts.OutcomeModels;
using AbstractTasksLogic.Services;
using AutoMapper;
using Contracts;
using Domain.Models;
using MassTransit;

namespace AbstractTasksLogic.Consumers;

public class NomenclatureCrudConsumer : BasicCrudConsumer<object>
{
    public NomenclatureCrudConsumer(
        IMapper mapper,
        ILogger<NomenclatureCrudConsumer> logger, 
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
                case UpsertCommand<NomenclatureModel> upsertCommand:
                    var upsertedNomenclature = await _nomenclatureService.UpsertNomenclatureAsync(upsertCommand.Data);
                    await context.RespondAsync(upsertedNomenclature);
                    break;
                    
                case BatchUpsertCommand<NomenclatureModel> batchUpsertCommand:
                    await _nomenclatureService.BatchUpsertNomenclaturesAsync(batchUpsertCommand.Data);
                    await context.RespondAsync(new { Success = true, Message = "Batch upsert completed" });
                    break;
                    
                case DeleteCommand<NomenclatureModel> deleteCommand:
                    await _nomenclatureService.DeleteNomenclatureAsync(deleteCommand.Id);
                    await context.RespondAsync(new { Success = true, Message = "Nomenclature deleted successfully" });
                    break;
                    
                case BatchDeleteCommand<NomenclatureModel> batchDeleteCommand:
                    await _nomenclatureService.BatchDeleteNomenclaturesAsync(batchDeleteCommand.Ids);
                    await context.RespondAsync(new { Success = true, Message = "Batch delete completed" });
                    break;
                    
                default:
                    await context.RespondAsync(new { Success = false, Message = "Unknown message type" });
                    break;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error processing nomenclature operation");
            await context.RespondAsync(new { Success = false, Message = e.Message });
        }
    }
}

