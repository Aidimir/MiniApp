using System.ComponentModel.DataAnnotations;

namespace Contracts.IncomeModels;

public record RestartTaskModel : BaseAuthTaskModel
{
    [Required(ErrorMessage = "TaskId is required.")]
    public required Guid TaskId { get; set; }
}