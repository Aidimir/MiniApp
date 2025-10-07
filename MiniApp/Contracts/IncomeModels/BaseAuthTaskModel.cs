using System.ComponentModel.DataAnnotations;

namespace Contracts.IncomeModels;

public record BaseAuthTaskModel
{
    [Required(ErrorMessage = "UserId is required.")]
    public required Guid UserId { get; init; }
}