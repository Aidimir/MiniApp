namespace Contracts.OutcomeModels;

public class TaskResponse
{
    public required Guid TaskId { get; set; }
    public required string Description { get; set; } = string.Empty;
    public required string Data { get; set; }
    public required int TTL { get; set; }
    public required string Status { get; set; }
    public required string StatusMessage { get; set; }
    public required DateTime? ExecutionStartedAt { get; set; }
    public required DateTime? ExecutionFinishedAt { get; set; }
}