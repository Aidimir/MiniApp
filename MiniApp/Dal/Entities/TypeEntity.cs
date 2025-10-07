using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dal.Entities;

public class TypeEntity
{
    [Key]
    public required Guid IDType { get; init; }

    public required string Name { get; set; }
    
    public int? IDParentType { get; set; }
}
