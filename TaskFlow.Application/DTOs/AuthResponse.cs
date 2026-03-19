namespace TaskFlow.Application.DTOs
{
    public record AuthResponse(Guid UserId, string Email, string Token);
}
