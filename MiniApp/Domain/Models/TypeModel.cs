namespace Domain.Models;

public class TypeModel
{
    public Guid IDType { get; set; }
    public required string Name { get; set; }
    public int? IDParentType { get; set; }
}