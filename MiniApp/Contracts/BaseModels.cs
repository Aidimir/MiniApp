namespace Contracts;

// Базовые модели
public abstract class BaseModel
{
    public Guid Id { get; set; }
}

// Модели для Type
public class TypeModel : BaseModel
{
    public required string Name { get; set; }
    public int? IDParentType { get; set; }
}

public class UpsertCommand<T>
{
    public required T Data { get; set; }
}

public class DeleteCommand<T>
{
    public Guid Id { get; set; }
}

public class BatchUpsertCommand<T>
{
    public required List<T> Data { get; set; }
}

public class BatchDeleteCommand<T>
{
    public required List<Guid> Ids { get; set; }
}