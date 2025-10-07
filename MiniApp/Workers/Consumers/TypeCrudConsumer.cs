using AbstractTasksLogic.Interfaces;
using Contracts.IncomeModels;
using Contracts.OutcomeModels;
using AbstractTasksLogic.Services;
using AutoMapper;
using Contracts;
using MassTransit;

namespace AbstractTasksLogic.Consumers;

public class TypeCrudConsumer : BasicCrudConsumer<object>
{
    public TypeCrudConsumer(
        IMapper mapper,
        ILogger<TypeCrudConsumer> logger, 
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
                case UpsertCommand<TypeModel> upsertCommand:
                    var upsertedType = await _typeService.UpsertTypeAsync(new Domain.Models.TypeModel() { IDType = upsertCommand.Data.Id, Name = upsertCommand.Data.Name, IDParentType = upsertCommand.Data.IDParentType });
                    await context.RespondAsync(upsertedType);
                    break;
                    
                case BatchUpsertCommand<TypeModel> batchUpsertCommand:
                    await _typeService.BatchUpsertTypesAsync(batchUpsertCommand.Data.Select(upsertCommand => new Domain.Models.TypeModel() { IDType = upsertCommand.Id, Name = upsertCommand.Name, IDParentType = upsertCommand.IDParentType }).ToList());
                    await context.RespondAsync(new { Success = true, Message = "Batch upsert completed" });
                    break;
                    
                case DeleteCommand<TypeModel> deleteCommand:
                    await _typeService.DeleteTypeAsync(deleteCommand.Id);
                    await context.RespondAsync(new { Success = true, Message = "Type deleted successfully" });
                    break;
                    
                case BatchDeleteCommand<TypeModel> batchDeleteCommand:
                    await _typeService.BatchDeleteTypesAsync(batchDeleteCommand.Ids);
                    await context.RespondAsync(new { Success = true, Message = "Batch delete completed" });
                    break;
                    
                default:
                    await context.RespondAsync(new { Success = false, Message = "Unknown message type" });
                    break;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error processing type operation");
            await context.RespondAsync(new { Success = false, Message = e.Message });
        }
    }
}
