namespace AbstractTasksLogic.Models;

public class CreateTypeModel
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public int? IDParentType { get; set; }
}