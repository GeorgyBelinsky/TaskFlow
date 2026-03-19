namespace TaskFlow.Application.DTOs
{
    public record ProjectResponse
    (
        Guid Id,
        string? ImageUrl,
        string Name = null!
    );
}
