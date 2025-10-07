using AbstractTasksDal.Interfaces;
using AbstractTasksLogic.Interfaces;
using AbstractTasksLogic.Models;
using Dal.Entities;
using Domain.Models;

namespace AbstractTasksLogic.Services;

public class TypeService : ITypeService
{
    private readonly ITypeContext _typeContext;

    public TypeService(ITypeContext typeContext)
    {
        _typeContext = typeContext;
    }

    public async Task<TypeModel> UpsertTypeAsync(TypeModel model)
    {
        try
        {
            // Пытаемся получить существующую запись
            var existingEntity = await _typeContext.GetTypeByIdAsync(model.IDType);
            
            // Если найдено, обновляем
            existingEntity.Name = model.Name;
            existingEntity.IDParentType = model.IDParentType;

            var result = await _typeContext.UpdateTypeAsync(existingEntity);
            return GetDomainModelFromEntity(result);
        }
        catch (KeyNotFoundException)
        {
            // Если не найдено, создаем новую с переданным ID
            var entityModel = new TypeEntity
            {
                IDType = model.IDType,
                Name = model.Name,
                IDParentType = model.IDParentType
            };

            var result = await _typeContext.AddTypeAsync(entityModel);
            return GetDomainModelFromEntity(result);
        }
    }

    public async Task BatchUpsertTypesAsync(List<TypeModel> models)
    {
        foreach (var model in models)
        {
            await UpsertTypeAsync(model);
        }
    }

    public async Task BatchDeleteTypesAsync(List<Guid> ids)
    {
        foreach (var id in ids)
        {
            await DeleteTypeAsync(id);
        }
    }

    public async Task DeleteTypeAsync(Guid id)
    {
        await _typeContext.RemoveTypeAsync(id);
    }

    public async Task<TypeModel> GetTypeByIdAsync(Guid id)
    {
        var entity = await _typeContext.GetTypeByIdAsync(id);
        return GetDomainModelFromEntity(entity);
    }

    public async Task<IEnumerable<TypeModel>> GetAllTypesAsync()
    {
        var entities = await _typeContext.GetAllTypesAsync();
        return entities.Select(GetDomainModelFromEntity).ToList();
    }

    private TypeModel GetDomainModelFromEntity(TypeEntity entity)
    {
        return new TypeModel
        {
            IDType = entity.IDType,
            Name = entity.Name,
            IDParentType = entity.IDParentType
        };
    }
}

public class NomenclatureService : INomenclatureService
{
    private readonly INomenclatureContext _nomenclatureContext;

    public NomenclatureService(INomenclatureContext nomenclatureContext)
    {
        _nomenclatureContext = nomenclatureContext;
    }

    public async Task<NomenclatureModel> UpsertNomenclatureAsync(NomenclatureModel model)
    {
        try
        {
            // Пытаемся получить существующую запись
            var existingEntity = await _nomenclatureContext.GetNomenclatureByIdAsync(model.ID);
            
            // Обновляем существующую запись
            existingEntity.IDCat = model.IDCat.ToString();
            existingEntity.IDTypeNew = model.IDTypeNew;
            existingEntity.ProductionType = model.ProductionType;
            existingEntity.IDFunctionType = model.IDFunctionType?.ToString();
            existingEntity.Name = model.Name;
            existingEntity.Gost = model.Gost;
            existingEntity.FormOfLength = model.FormOfLength;
            existingEntity.Manufacturer = model.Manufacturer;
            existingEntity.SteelGrade = model.SteelGrade;
            existingEntity.Diameter = model.Diameter;
            existingEntity.ProfileSize2 = model.ProfileSize2;
            existingEntity.PipeWallThickness = model.PipeWallThickness;
            existingEntity.Status = model.Status;
            existingEntity.Koef = model.Koef;

            var result = await _nomenclatureContext.UpdateNomenclatureAsync(existingEntity);
            return GetDomainModelFromEntity(result);
        }
        catch (KeyNotFoundException)
        {
            // Если не найдено, создаем новую с переданным ID
            var entityModel = new NomenclatureEntity
            {
                ID = model.ID,
                IDCat = model.IDCat.ToString(),
                IDTypeNew = model.IDTypeNew,
                ProductionType = model.ProductionType,
                IDFunctionType = model.IDFunctionType?.ToString(),
                Name = model.Name,
                Gost = model.Gost,
                FormOfLength = model.FormOfLength,
                Manufacturer = model.Manufacturer,
                SteelGrade = model.SteelGrade,
                Diameter = model.Diameter,
                ProfileSize2 = model.ProfileSize2,
                PipeWallThickness = model.PipeWallThickness,
                Status = model.Status,
                Koef = model.Koef
            };

            var result = await _nomenclatureContext.AddNomenclatureAsync(entityModel);
            return GetDomainModelFromEntity(result);
        }
    }

    public async Task BatchUpsertNomenclaturesAsync(List<NomenclatureModel> models)
    {
        foreach (var model in models)
        {
            await UpsertNomenclatureAsync(model);
        }
    }

    public async Task BatchDeleteNomenclaturesAsync(List<Guid> ids)
    {
        foreach (var id in ids)
        {
            await DeleteNomenclatureAsync(id);
        }
    }

    public async Task DeleteNomenclatureAsync(Guid id)
    {
        var stringId = id.ToString();
        await _nomenclatureContext.RemoveNomenclatureAsync(stringId);
    }

    public async Task<NomenclatureModel> GetNomenclatureByIdAsync(Guid id)
    {
        var stringId = id.ToString();
        var entity = await _nomenclatureContext.GetNomenclatureByIdAsync(stringId);
        return GetDomainModelFromEntity(entity);
    }

    public async Task<IEnumerable<NomenclatureModel>> GetAllNomenclaturesAsync()
    {
        var entities = await _nomenclatureContext.GetAllNomenclaturesAsync();
        return entities.Select(GetDomainModelFromEntity).ToList();
    }

    public async Task<IEnumerable<NomenclatureModel>> GetNomenclaturesByTypeAsync(Guid typeId)
    {
        var allNomenclatures = await _nomenclatureContext.GetAllNomenclaturesAsync();
        var filtered = allNomenclatures.Where(n => n.IDTypeNew == typeId.ToString());
        return filtered.Select(GetDomainModelFromEntity).ToList();
    }

    private NomenclatureModel GetDomainModelFromEntity(NomenclatureEntity entity)
    {
        return new NomenclatureModel
        {
            ID = entity.ID,
            IDCat = entity.IDCat,
            IDType = entity.IDTypeNew,
            IDTypeNew = entity.IDTypeNew,
            ProductionType = entity.ProductionType,
            IDFunctionType = int.TryParse(entity.IDFunctionType, out var functionId) ? functionId : null,
            Name = entity.Name,
            Gost = entity.Gost,
            FormOfLength = entity.FormOfLength,
            Manufacturer = entity.Manufacturer,
            SteelGrade = entity.SteelGrade,
            Diameter = entity.Diameter,
            ProfileSize2 = entity.ProfileSize2,
            PipeWallThickness = entity.PipeWallThickness,
            Status = entity.Status,
            Koef = entity.Koef
        };
    }
}

public class PriceService : IPriceService
{
    private readonly IPriceContext _priceContext;
    private readonly IStockContext _stockContext;

    public PriceService(IPriceContext priceContext, IStockContext stockContext)
    {
        _priceContext = priceContext;
        _stockContext = stockContext;
    }

    public async Task<PriceModel> UpsertPriceAsync(PriceModel model)
    {
        try
        {
            // Пытаемся получить существующую запись
            var existingEntity = await _priceContext.GetPriceByIdAsync(model.ID, model.IDStock);
            
            // Обновляем существующую запись
            existingEntity.PriceT = model.PriceT;
            existingEntity.PriceLimitT1 = model.PriceLimitT1;
            existingEntity.PriceT1 = model.PriceT1;
            existingEntity.PriceLimitT2 = model.PriceLimitT2;
            existingEntity.PriceT2 = model.PriceT2;
            existingEntity.PriceM = model.PriceM;
            existingEntity.PriceLimitM1 = model.PriceLimitM1;
            existingEntity.PriceM1 = model.PriceM1;
            existingEntity.PriceLimitM2 = model.PriceLimitM2;
            existingEntity.PriceM2 = model.PriceM2;
            existingEntity.NDS = int.Parse(model.NDS);

            var result = await _priceContext.UpdatePriceAsync(existingEntity);
            return GetDomainModelFromEntity(result);
        }
        catch (KeyNotFoundException)
        {
            // Получаем StockEntity по IDStock
            var stock = await _stockContext.GetStockByIdAsync(model.IDStock);
            
            // Если не найдено, создаем новую с переданным ID
            var entityModel = new PriceEntity
            {
                ID = model.ID,
                PriceT = model.PriceT,
                PriceLimitT1 = model.PriceLimitT1,
                PriceT1 = model.PriceT1,
                PriceLimitT2 = model.PriceLimitT2,
                PriceT2 = model.PriceT2,
                PriceM = model.PriceM,
                PriceLimitM1 = model.PriceLimitM1,
                PriceM1 = model.PriceM1,
                PriceLimitM2 = model.PriceLimitM2,
                PriceM2 = model.PriceM2,
                NDS = int.Parse(model.NDS),
                Stock = stock,
                IDStock = stock.IDStock
            };

            var result = await _priceContext.AddPriceAsync(entityModel);
            return GetDomainModelFromEntity(result);
        }
    }

    public async Task BatchUpsertPricesAsync(List<PriceModel> models)
    {
        foreach (var model in models)
        {
            await UpsertPriceAsync(model);
        }
    }

    public async Task BatchDeletePricesAsync(List<Guid> ids)
    {
        foreach (var id in ids)
        {
            await DeletePriceAsync(id);
        }
    }

    public async Task DeletePriceAsync(Guid id)
    {
        // Для удаления цены нужен stockId, но в интерфейсе только id
        // Нужно либо изменить интерфейс, либо получить stockId из базы
        var prices = await _priceContext.GetPricesByNomenclatureAsync(id);
        foreach (var price in prices)
        {
            await _priceContext.RemovePriceAsync(price.ID, price.Stock.IDStock);
        }
    }

    public async Task<PriceModel> GetPriceByIdAsync(Guid id)
    {
        // Аналогичная проблема - нужен stockId
        var prices = await _priceContext.GetPricesByNomenclatureAsync(id);
        var price = prices.FirstOrDefault();
        if (price == null)
            throw new KeyNotFoundException($"Price with id {id} not found.");
        
        return GetDomainModelFromEntity(price);
    }

    public async Task<IEnumerable<PriceModel>> GetAllPricesAsync()
    {
        // Этот метод требует доработки контекста
        throw new NotImplementedException("GetAllPricesAsync requires context implementation");
    }

    public async Task<IEnumerable<PriceModel>> GetPricesByNomenclatureAsync(Guid nomenclatureId)
    {
        var entities = await _priceContext.GetPricesByNomenclatureAsync(nomenclatureId);
        return entities.Select(GetDomainModelFromEntity).ToList();
    }

    public async Task<IEnumerable<PriceModel>> GetPricesByStockAsync(Guid stockId)
    {
        var entities = await _priceContext.GetPricesByStockAsync(stockId);
        return entities.Select(GetDomainModelFromEntity).ToList();
    }

    private PriceModel GetDomainModelFromEntity(PriceEntity entity)
    {
        return new PriceModel
        {
            ID = entity.ID,
            IDStock = entity.Stock?.IDStock ?? Guid.Empty,
            PriceT = entity.PriceT,
            PriceLimitT1 = entity.PriceLimitT1,
            PriceT1 = entity.PriceT1,
            PriceLimitT2 = entity.PriceLimitT2,
            PriceT2 = entity.PriceT2,
            PriceM = entity.PriceM,
            PriceLimitM1 = entity.PriceLimitM1,
            PriceM1 = entity.PriceM1,
            PriceLimitM2 = entity.PriceLimitM2,
            PriceM2 = entity.PriceM2,
            NDS = entity.NDS.ToString()
        };
    }
}

public class RemnantService : IRemnantService
{
    private readonly IRemnantContext _remnantContext;
    private readonly IStockContext _stockContext;

    public RemnantService(IRemnantContext remnantContext, IStockContext stockContext)
    {
        _remnantContext = remnantContext;
        _stockContext = stockContext;
    }

    public async Task<RemnantModel> UpsertRemnantAsync(RemnantModel model)
    {
        try
        {
            // Пытаемся получить существующую запись
            var existingEntity = await _remnantContext.GetRemnantByIdAsync(model.ID, model.IDStock);
            
            // Обновляем существующую запись
            existingEntity.InStockT = model.InStockT;
            existingEntity.InStockM = model.InStockM;
            existingEntity.SoonArriveT = model.SoonArriveT;
            existingEntity.SoonArriveM = model.SoonArriveM;
            existingEntity.ReservedT = model.ReservedT;
            existingEntity.ReservedM = model.ReservedM;
            existingEntity.UnderTheOrder = model.UnderTheOrder;
            existingEntity.AvgTubeLength = model.AvgTubeLength;
            existingEntity.AvgTubeWeight = model.AvgTubeWeight;

            var result = await _remnantContext.UpdateRemnantAsync(existingEntity);
            return GetDomainModelFromEntity(result);
        }
        catch (KeyNotFoundException)
        {
            // Получаем StockEntity по IDStock
            var stock = await _stockContext.GetStockByIdAsync(model.IDStock);
            
            // Если не найдено, создаем новую с переданным ID
            var entityModel = new RemnantEntity
            {
                ID = model.ID,
                InStockT = model.InStockT,
                InStockM = model.InStockM,
                SoonArriveT = model.SoonArriveT,
                SoonArriveM = model.SoonArriveM,
                ReservedT = model.ReservedT,
                ReservedM = model.ReservedM,
                UnderTheOrder = model.UnderTheOrder,
                AvgTubeLength = model.AvgTubeLength,
                AvgTubeWeight = model.AvgTubeWeight,
                Stock = stock
            };

            var result = await _remnantContext.AddRemnantAsync(entityModel);
            return GetDomainModelFromEntity(result);
        }
    }

    public async Task BatchUpsertRemnantsAsync(List<RemnantModel> models)
    {
        foreach (var model in models)
        {
            await UpsertRemnantAsync(model);
        }
    }

    public async Task BatchDeleteRemnantsAsync(List<Guid> ids)
    {
        foreach (var id in ids)
        {
            await DeleteRemnantAsync(id);
        }
    }

    public async Task DeleteRemnantAsync(Guid id)
    {
        // Для удаления остатка нужен stockId, но в интерфейсе только id
        var remnants = await _remnantContext.GetRemnantsByStockAsync(id);
        foreach (var remnant in remnants)
        {
            await _remnantContext.RemoveRemnantAsync(remnant.ID, remnant.Stock.IDStock);
        }
    }

    public async Task<RemnantModel> GetRemnantByIdAsync(Guid id)
    {
        // Аналогичная проблема - нужен stockId
        throw new NotImplementedException("GetRemnantByIdAsync requires stockId parameter");
    }

    public async Task<IEnumerable<RemnantModel>> GetAllRemnantsAsync()
    {
        // Этот метод требует доработки контекста
        throw new NotImplementedException("GetAllRemnantsAsync requires context implementation");
    }

    public async Task<IEnumerable<RemnantModel>> GetRemnantsByNomenclatureAsync(Guid nomenclatureId)
    {
        // Этот метод требует доработки контекста
        throw new NotImplementedException("GetRemnantsByNomenclatureAsync requires context implementation");
    }

    public async Task<IEnumerable<RemnantModel>> GetRemnantsByStockAsync(Guid stockId)
    {
        var entities = await _remnantContext.GetRemnantsByStockAsync(stockId);
        return entities.Select(GetDomainModelFromEntity).ToList();
    }

    public async Task<IEnumerable<RemnantModel>> GetRemnantsWithStockInfoAsync(Guid nomenclatureId)
    {
        // Реализация требует объединения данных из разных контекстов
        throw new NotImplementedException("GetRemnantsWithStockInfoAsync requires additional implementation");
    }

    private RemnantModel GetDomainModelFromEntity(RemnantEntity entity)
    {
        return new RemnantModel
        {
            ID = entity.ID,
            IDStock = entity.Stock?.IDStock ?? Guid.Empty,
            InStockT = entity.InStockT,
            InStockM = entity.InStockM,
            SoonArriveT = entity.SoonArriveT,
            SoonArriveM = entity.SoonArriveM,
            ReservedT = entity.ReservedT,
            ReservedM = entity.ReservedM,
            UnderTheOrder = entity.UnderTheOrder,
            AvgTubeLength = entity.AvgTubeLength,
            AvgTubeWeight = entity.AvgTubeWeight
        };
    }
}

public class StockService : IStockService
{
    private readonly IStockContext _stockContext;

    public StockService(IStockContext stockContext)
    {
        _stockContext = stockContext;
    }

    public async Task<StockModel> UpsertStockAsync(StockModel model)
    {
        try
        {
            // Пытаемся получить существующую запись
            var existingEntity = await _stockContext.GetStockByIdAsync(model.IDStock);
            
            // Обновляем существующую запись
            existingEntity.IDDivision = model.IDDivision ?? Guid.Empty;
            existingEntity.Stock = model.Stock;
            existingEntity.StockName = model.StockName;
            existingEntity.Address = model.Address;
            existingEntity.Schedule = model.Schedule;
            existingEntity.CashPayment = model.CashPayment ?? false;
            existingEntity.CardPayment = model.CardPayment ?? false;
            existingEntity.FIASId = Guid.Parse(model.FIASId ?? Guid.Empty.ToString());
            existingEntity.OwnerInn = model.OwnerInn ?? string.Empty;
            existingEntity.OwnerKpp = model.OwnerKpp ?? string.Empty;
            existingEntity.OwnerFullName = model.OwnerFullName ?? string.Empty;
            existingEntity.OwnerShortName = model.OwnerShortName ?? string.Empty;
            existingEntity.RailwayStation = model.RailwayStation ?? string.Empty;
            existingEntity.ConsigneeCode = model.ConsigneeCode ?? string.Empty;

            var result = await _stockContext.UpdateStockAsync(existingEntity);
            return GetDomainModelFromEntity(result);
        }
        catch (KeyNotFoundException)
        {
            // Если не найдено, создаем новую с переданным ID
            var entityModel = new StockEntity
            {
                IDStock = model.IDStock,
                IDDivision = model.IDDivision ?? Guid.Empty,
                Stock = model.Stock,
                StockName = model.StockName,
                Address = model.Address,
                Schedule = model.Schedule,
                CashPayment = model.CashPayment ?? false,
                CardPayment = model.CardPayment ?? false,
                FIASId = Guid.Parse(model.FIASId ?? Guid.Empty.ToString()),
                OwnerInn = model.OwnerInn ?? string.Empty,
                OwnerKpp = model.OwnerKpp ?? string.Empty,
                OwnerFullName = model.OwnerFullName ?? string.Empty,
                OwnerShortName = model.OwnerShortName ?? string.Empty,
                RailwayStation = model.RailwayStation ?? string.Empty,
                ConsigneeCode = model.ConsigneeCode ?? string.Empty
            };

            var result = await _stockContext.AddStockAsync(entityModel);
            return GetDomainModelFromEntity(result);
        }
    }

    public async Task BatchUpsertStocksAsync(List<StockModel> models)
    {
        foreach (var model in models)
        {
            await UpsertStockAsync(model);
        }
    }

    public async Task BatchDeleteStocksAsync(List<Guid> ids)
    {
        foreach (var id in ids)
        {
            await DeleteStockAsync(id);
        }
    }

    public async Task DeleteStockAsync(Guid id)
    {
        await _stockContext.RemoveStockAsync(id);
    }

    public async Task<StockModel> GetStockByIdAsync(Guid id)
    {
        var entity = await _stockContext.GetStockByIdAsync(id);
        return GetDomainModelFromEntity(entity);
    }

    public async Task<IEnumerable<StockModel>> GetAllStocksAsync()
    {
        var entities = await _stockContext.GetAllStocksAsync();
        return entities.Select(GetDomainModelFromEntity).ToList();
    }

    public async Task<IEnumerable<StockModel>> GetStocksByCityAsync(string city)
    {
        var entities = await _stockContext.GetStocksByCityAsync(city);
        return entities.Select(GetDomainModelFromEntity).ToList();
    }

    private StockModel GetDomainModelFromEntity(StockEntity entity)
    {
        return new StockModel
        {
            IDStock = entity.IDStock,
            Stock = entity.Stock,
            StockName = entity.StockName,
            Address = entity.Address,
            Schedule = entity.Schedule,
            IDDivision = entity.IDDivision,
            CashPayment = entity.CashPayment,
            CardPayment = entity.CardPayment,
            FIASId = entity.FIASId.ToString(),
            OwnerInn = entity.OwnerInn,
            OwnerKpp = entity.OwnerKpp,
            OwnerFullName = entity.OwnerFullName,
            OwnerShortName = entity.OwnerShortName,
            RailwayStation = entity.RailwayStation,
            ConsigneeCode = entity.ConsigneeCode
        };
    }
}