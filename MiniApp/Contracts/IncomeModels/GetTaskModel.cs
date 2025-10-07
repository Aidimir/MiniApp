using System.ComponentModel.DataAnnotations;

namespace Contracts.IncomeModels;

public record GetTaskModel : BaseAuthTaskModel
{
    [Required(ErrorMessage = "TaskId is required.")]
    public required Guid? TaskId { get; set; }
}