namespace TaskFlow.Application.DTOs
{
    public record ProjectStatsResponse
    (
         int TotalTasks,
         int Todo,
         int InProgress,
         int Done
    );
}
