namespace Contracts.OutcomeModels;

public record RestartTaskResponse
{
    public required Guid TaskId { get; set; }
}