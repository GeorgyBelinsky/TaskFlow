namespace TaskFlow.Application.DTOs
{
    public class UploadProjectImageRequest
    {
        public IFormFile Image { get; set; } = null!;
    }
}
