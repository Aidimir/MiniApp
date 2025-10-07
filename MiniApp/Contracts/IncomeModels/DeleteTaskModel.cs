using System.ComponentModel.DataAnnotations;

namespace Contracts.IncomeModels;

public record DeleteTaskModel : BaseAuthTaskModel
{
    [Required(ErrorMessage = "TaskId is required.")]
    [StringLength(36, ErrorMessage = "TaskId must be 36 characters long.")]
    public required Guid TaskId { get; init; }
}